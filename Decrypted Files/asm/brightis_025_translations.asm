.psx
.open "OVR\025.bin", 0x80158138

.org 0x8015837C
	addiu a0, a0, -0x7EC8
.org 0x80158138
	.ascii "El Saad"
	.byte 0, 0

.close