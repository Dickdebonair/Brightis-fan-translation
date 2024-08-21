def decompress(input_bytes, output_bytes):
		current_byte = input_bytes[0]
		global_pointer = 1  # Equivalent to DAT_800aea88
		output_pointer = 0  # Equivalent to DAT_800aea94

		while current_byte != 0:
				if (current_byte & 0x80) == 0:
						if (current_byte & 0x40) == 0:
								if (current_byte & 0x20) == 0:
										count = current_byte & 0x1f
								else:
										current_byte = input_bytes[global_pointer]
										global_pointer += 1
										count = current_byte | (current_byte & 0x1f) << 8
								if count != 0:
										for _ in range(count):
												current_byte = input_bytes[global_pointer]
												global_pointer += 1
												output_bytes[output_pointer] = current_byte
												output_pointer += 1
						else:
								count = current_byte & 0xf
								if (current_byte & 0x10) != 0:
										current_byte = input_bytes[global_pointer]
										global_pointer += 1
										count = current_byte | count << 8
								count += 4
								current_byte = input_bytes[global_pointer]
								global_pointer += 1
								if count != 0:
										for _ in range(count):
												output_bytes[output_pointer] = current_byte
												count -= 1
												output_pointer += 1
				else:
						count = (current_byte >> 5) | 4
						current_byte = input_bytes[global_pointer]
						global_pointer += 1
						reference_pointer = output_pointer - (current_byte | (current_byte & 0x1f) << 8)
						if count != 0:
								for _ in range(count):
										current_byte = output_bytes[reference_pointer]
										reference_pointer += 1
										count -= 1
										output_bytes[output_pointer] = current_byte
										output_pointer += 1
						current_byte = input_bytes[global_pointer]
						while (current_byte & 0xe0) == 0x60:
								count = current_byte & 0x1f
								global_pointer += 1
								if (current_byte & 0x1f) != 0:
										for _ in range(count):
												current_byte = output_bytes[reference_pointer]
												reference_pointer += 1
												count -= 1
												output_bytes[output_pointer] = current_byte
												output_pointer += 1
								current_byte = input_bytes[global_pointer]
				current_byte = input_bytes[global_pointer]
				global_pointer += 1

		some_global_variable = output_pointer - len(output_bytes)  # Equivalent to DAT_800aeb2c