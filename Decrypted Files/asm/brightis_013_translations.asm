.psx
.open "OVR\013.bin", 0x80158138

.org 0x801586DC
	addiu a0, a0, -0x7EC8
.org 0x80158138
	.ascii "To go up, hold the L2 button "
	.byte 78
	.ascii " & make a series of jumps "
	.byte 0, 0

.org 0x80159224
	addiu a0, a0, -0x7E88
.org 0x80158178
	.ascii "Dungeon Boots"
	.byte 0, 0

.close