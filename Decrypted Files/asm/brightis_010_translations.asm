.psx
.open "OVR\010.bin", 0x80158138

.org 0x8015D770
	.word 0x80158138
.org 0x80159C7C
	jal 0x8015D958
.org 0x80158138
	.byte 44
	.ascii "3 Days Later..."
	.byte 0, 0

.org 0x8015D774
	.word 0x8015D978
.org 0x80159CEC
	jal 0x8015D958
.org 0x8015D978
	.ascii "City of El Saad"
	.byte 0, 0

.close