.psx
.open "OVR\029.bin", 0x80158138

.org 0x8015906C
	addiu a0, a0, -0x7EC8
.org 0x80158138
	.ascii "Do you want to give up?"
	.byte 0, 0

.org 0x801582C4
	addiu a0, a0, -0x364C
.org 0x801582CC
	addiu a0, a0, -0x364C
.org 0x8015C9B4
	.ascii "El Saad"
	.byte 0, 0

.close