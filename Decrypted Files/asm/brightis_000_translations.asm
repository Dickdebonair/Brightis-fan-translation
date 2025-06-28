.psx
.open "OVR\000.bin", 0x80158138

.org 0x0015ED70
	addiu a0, a0, -0x1384
.org 0x8015EC7C
	.ascii "  Bonus floor! Advance to the next floor. "
	.byte 7
	.ascii "     Some enemies drop items "
	.byte 0, 0

.org 0x0015ED64
	addiu a0, a0, -0x1338
.org 0x8015ECC8
	.byte 7
	.ascii " Open all treasure chests "
	.byte 0, 0

.org 0x0015ED94
	addiu a0, a0, -0x130C
.org 0x8015ECF4
	.ascii "     Take the elements held by the enemy"
	.byte 7
	.ascii "       Light the candlestick "
	.byte 0, 0

.org 0x0015ED8C
	addiu a0, a0, -0x12C4
.org 0x8015ED3C
	.byte 7
	.ascii " Go round all the rooms "
	.byte 0, 0

.org 0x0015ED68
	addiu a0, a0, -0x0CF0
.org 0x8015F310
	.byte 7
	.ascii " Defeat all enemies "
	.byte 0, 0

.close