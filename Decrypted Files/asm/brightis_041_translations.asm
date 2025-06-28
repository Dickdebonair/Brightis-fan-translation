.psx
.open "OVR\041.bin", 0x80158138

.org 0x80158A30
	addiu a0, a0, -0x538C
.org 0x8015AC74
	.ascii "Caplas"
	.byte 7
	.ascii "　...go back to the city for now. "
	.byte 11, 15, 5
	.ascii "　My best friend "
	.byte 11, 15, 7
	.ascii "　is in town, along with those "
	.byte 11, 15, 7
	.ascii "　who were once my rivals. "
	.byte 11, 15, 5
	.ascii "　Gain experience & learn"
	.byte 11, 15, 7
	.ascii " techniques that suit you."
	.byte 11, 15, 7
	.ascii "　So you can become strong. "
	.byte 11, 15, 5
	.ascii "　...Soon we'll avenge our friends! "
	.byte 11, 15, 4
	.ascii "　...As the captain-"
	.byte 11, 15, 7
	.ascii "　I have a duty to rescue "
	.byte 11, 15, 7
	.ascii "　any survivors left behind."
	.byte 11, 15, 4
	.ascii "　Don't worry about me. "
	.byte 11, 15, 7
	.ascii "　Come now, "
	.byte 10
	.ascii ". "
	.byte 11, 15, 7
	.ascii "　Head north. Get going! "
	.byte 11, 15, 0

.org 0x80158534
	addiu a0, a0, -0x51F0
.org 0x8015AE10
	.ascii "　Defeat them "
	.byte 11, 15, 7
	.ascii " & clear a path to the exit. "
	.byte 11, 15, 5
	.ascii "　You're still a rookie, "
	.byte 11, 15, 7
	.ascii "　so I'll help you. "
	.byte 11, 15, 4
	.ascii "　Did you forget how to block? "
	.byte 11, 15, 7
	.ascii "　Block if you want to live! "
	.byte 11, 15, 4
	.ascii "　Alright let's go. Cover me! "
	.byte 11, 15, 0

.org 0x80158878
	addiu a0, a0, -0x5128
.org 0x8015AED8
	.ascii "Caplas "
	.byte 7
	.ascii " You're still in danger, "
	.byte 10
	.ascii ". "
	.byte 11, 15, 7
	.ascii " You've got gaps above you. "
	.byte 11, 15, 7
	.ascii " I'll teach you a trick."
	.byte 11, 15, 5
	.ascii " Use it with the TECH button. "
	.byte 11, 15, 7
	.ascii " You'll have to time it. "
	.byte 11, 15, 0

.org 0x801584F8
	addiu a0, a0, -0x5088
.org 0x8015AF78
	.ascii "Caplas"
	.byte 7
	.ascii "　..."
	.byte 10
	.ascii ", was it? "
	.byte 11, 15, 7
	.ascii "　Are you the only survivor? "
	.byte 11, 15, 4
	.ascii " Well, Wado was deeply wounded..."
	.byte 11, 15, 7
	.ascii " I doubt that even"
	.byte 11, 15, 7
	.ascii " Regi could've made it..."
	.byte 11, 15, 0

.org 0x80158534
	addiu a0, a0, -0x4FF8
.org 0x8015B008
	.ascii "Caplas"
	.byte 7
	.ascii "　But apparently you're lucky. "
	.byte 11, 15, 7
	.ascii "　Get out of here ASAP"
	.byte 11, 15, 7
	.ascii " and report back home. "
	.byte 11, 15, 5
	.ascii "　...I've got some things to do. "
	.byte 11, 15, 0

.org 0x80158AF4
	addiu a0, a0, -0x4F78
.org 0x8015B088
	.ascii "Caplas "
	.byte 7
	.ascii "　Defend yourself! "
	.byte 10
	.ascii "! "
	.byte 11, 15, 7
	.ascii "　Don't turn your back on them!"
	.byte 1, 15, 0

.org 0x80158BE0
	addiu a0, a0, -0x4F34
.org 0x8015B0CC
	.ascii "Caplas"
	.byte 7
	.ascii "　You are my last man... "
	.byte 11, 15, 7
	.ascii "　Don't die. "
	.byte 11, 15, 0

.org 0x801584DC
	addiu a0, a0, -0x4F00
.org 0x8015B100
	.ascii "Caplas "
	.byte 7
	.ascii "　You are...? "
	.byte 11, 15, 0

.org 0x801589EC
	addiu a0, a0, -0x4EE4
.org 0x8015B11C
	.byte 7, 10
	.ascii " learned \"Expose\"!"
	.byte 11, 15, 0

.org 0x801586F4
	addiu a0, a0, -0x4ECC
.org 0x80158714
	jal 0x80155D3C
.org 0x8015B134
	.ascii "ACTION Button"
	.byte 0

.org 0x80158720
	addiu a0, a0, -0x4EBC
.org 0x80158730
	jal 0x80155D3C
.org 0x8015B144
	.ascii "\"Expose\""
	.byte 0

.close