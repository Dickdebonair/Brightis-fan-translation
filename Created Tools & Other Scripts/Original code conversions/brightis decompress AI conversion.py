def decompress(param_1, param_2):
	global DAT_800aea88, DAT_800aea94, DAT_800aeb2c

	bVar1 = param_1[0]
	DAT_800aea88 = param_1[1:]
	DAT_800aea94 = param_2

	while bVar1 != 0:
			uVar3 = bVar1
			if (bVar1 & 0x80) == 0:
					if (bVar1 & 0x40) == 0:
							if (bVar1 & 0x20) == 0:
									uVar3 &= 0x1f
							else:
									bVar1 = DAT_800aea88[0]
									DAT_800aea88 = DAT_800aea88[1:]
									uVar3 = bVar1 | (uVar3 & 0x1f) << 8
							if uVar3 != 0:
									for _ in range(uVar3):
											bVar1 = DAT_800aea88[0]
											DAT_800aea88 = DAT_800aea88[1:]
											DAT_800aea94[0] = bVar1
											DAT_800aea94 += 1
			else:
					uVar3
					uVar2 = (bVar1 >> 5) | 4
					bVar1 = DAT_800aea88[0]
					DAT_800aea88 = DAT_800aea88[1:]
					pbVar5 = DAT_800aea94 - ((bVar1 | (uVar3 & 0x1f) << 8))
					if uVar2 != 0:
							for _ in range(uVar2):
									bVar1 = pbVar5[0]
									pbVar5 += 1
									DAT_800aea94[0] = bVar1
									DAT_800aea94 += 1
					bVar1 = DAT_800aea88[0]
					while (bVar1 & 0xe0) == 0x60:
							uVar3 = bVar1 & 0x1f
							DAT_800aea88 = DAT_800aea88[1:]
							if (bVar1 & 0x1f) != 0:
									for _ in range(uVar3):
											bVar1 = pbVar5[0]
											pbVar5 += 1
											DAT_800aea94[0] = bVar1
											DAT_800aea94 += 1
							bVar1 = DAT_800aea88[0]
			bVar1 = DAT_800aea88[0]
			DAT_800aea88 = DAT_800aea88[1:]

	DAT_800aeb2c = len(DAT_800aea94) - len(param_2)

