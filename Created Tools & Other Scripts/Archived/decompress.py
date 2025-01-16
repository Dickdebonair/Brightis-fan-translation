from pathlib import Path
import argparse


def decompress( start_offset:int, source:Path, destination:Path):
    input_pos = start_offset
    output_pos = 0

    with open(source, 'rb') as f:
        input = list(f.read())
        input_size = len(input)

        #Initialize basic output (size is wrong but we fix it later)
        output = [0x0] * input_size * 3
        current_val = input[input_pos]
        input_pos += 1
        var3 = 0
        output_debug = []

        while current_val != 0:
            nb_raw_bytes = current_val

            if current_val & 0x80 == 0:
                if current_val & 0x40 == 0:
                    if current_val & 0x20 == 0:
                        nb_raw_bytes = current_val & 0x1F
                    else:
                        current_val = input[input_pos]
                        input_pos += 1
                        nb_raw_bytes = current_val | (var3 & 0x1F) << 8

                    # Extract Raw Bytes
                    if nb_raw_bytes > 0:                                                        #80020290
                        raw_bytes = input[input_pos:input_pos + nb_raw_bytes]    #80020298 to 800202AC
                        output[output_pos:output_pos+nb_raw_bytes] = raw_bytes
                        input_pos += nb_raw_bytes
                        output_pos += nb_raw_bytes
                        output_debug.append( ('Decomp', [hex(ele) for ele in raw_bytes]))

                else:
                    nb_raw_bytes = nb_raw_bytes & 0xF

                    if current_val & 0x10 != 0:                                         #80020224
                        current_val = input[input_pos]
                        input_pos += 1
                        nb_raw_bytes = current_val | nb_raw_bytes << 8

                    nb_raw_bytes += 4
                    current_val = input[input_pos]
                    input_pos += 1

                    # a2 seems to be
                    # number of time we want to repeat current_val in output
                    # we take current_val and we copy it x times where x = a2
                    if nb_raw_bytes != 0:                                                         #8002024C
                        output[output_pos:output_pos+nb_raw_bytes] = [current_val] * nb_raw_bytes    #until
                        output_pos += nb_raw_bytes
                        output_debug.append(('Decomp', [hex(ele) for ele in [current_val] * nb_raw_bytes]))

            else:       # Sliding window with Distance + Length based on current pos
                length = current_val >> 5 | 4
                current_val = input[input_pos]
                input_pos += 1
                distance = current_val | (nb_raw_bytes & 0x1F) << 8
                offset = output_pos - distance

                if length > 0:                                                              #800201AC until 800201C0

                    for i in range(length):
                        output[output_pos] = output[offset + i]
                        output_pos += 1

                    output_debug.append(('Sliding', [hex(ele) for ele in output[offset:offset + length]]))

                current_val = input[input_pos]

                seq = 0
                while current_val & 0xE0 == 0x60:

                    nb_raw_bytes = current_val & 0x1F
                    input_pos += 1

                    if nb_raw_bytes != 0:

                        for _ in range(nb_raw_bytes):
                            val_output = output[offset + length + seq]
                            output[output_pos] = val_output
                            output_pos += 1
                            seq += 1

                        output_debug.append(('Sliding+', [hex(ele) for ele in output[offset + length:offset + length + nb_raw_bytes]]))

                    current_val = input[input_pos]

            current_val = input[input_pos]
            input_pos += 1

    #Adjust output size based on output_pos
    output = output[:output_pos]

    with open(destination, 'wb') as dest:
        dest.write(bytes(output))
        print(f'File {destination.name} has been created')



def get_arguments(argv=None):
    # Init argument parser
    parser = argparse.ArgumentParser()

    parser.add_argument(
        "-a",
        "--action",
        required=True,
        choices=["d","c"],
        help="(Required) - Options: d"

    )
    parser.add_argument(
        "-s",
        "--source",
        required=True,
        type=Path,
        metavar="source",
        help="source file",
    )

    parser.add_argument(
        "-d",
        "--destination",
        required=True,
        type=Path,
        metavar="destination",
        help="destination file that will be created",
    )

    parser.add_argument(
        "-st",
        "--start",
        required=True,
        metavar="start",
        help="Starting offset in the file to start decompressing in HEX without 0x, ex: 3C",
    )

    args = parser.parse_args()

    return args

if __name__ == "__main__":
    args = get_arguments()
    start = int(args.start, 16)
    decompress(start_offset=start, source=Path(args.source), destination=Path(args.destination))