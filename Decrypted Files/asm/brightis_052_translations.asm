.psx
.open "OVR\052.bin", 0x80158138

.org 0x8015998C
	addiu a0, a0, -0x7EC8
.org 0x80158138
	.ascii "Witch "
	.byte 7
	.ascii " Welcome back, "
	.byte 10
	.ascii ". "
	.byte 11, 15, 7
	.ascii " Looks like it's all over. "
	.byte 11, 15, 4
	.ascii " This incident caused by"
	.byte 11, 25, 7
	.ascii " the Global Elements, they're "
	.byte 11, 15, 7
	.ascii " essential to life here. "
	.byte 11, 15, 4
	.ascii "It's why we mustn't misuse them. "
	.byte 11, 15, 7
	.ascii "That's what I learned from this. "
	.byte 11, 15, 7
	.ascii "It's something we can't forget. "
	.byte 11, 15, 4
	.ascii " There was a time when we lived "
	.byte 11, 15, 7
	.ascii " with energy like the elements."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 25, 7
	.ascii " People waged war here, "
	.byte 11, 15, 4
	.ascii " And almost went extinct."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 25, 7
	.ascii " But the few who survived"
	.byte 11, 15, 7
	.ascii " made this land what it is. "
	.byte 11, 15, 4
	.ascii " People are unstable creatures. "
	.byte 11, 15, 7
	.ascii " So someone has to be a beacon. "
	.byte 11, 15, 7
	.ascii " I will be that beacon here. "
	.byte 11, 15, 4
	.ascii " Thanks for everything. "
	.byte 11, 15, 7
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "See you again. "
	.byte 11, 15, 0, 0

.org 0x801599A0
	addiu a0, a0, -0x7C68
.org 0x80158398
	.ascii "Acre"
	.byte 7, 17, 0
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "Thank God, you're safe. "
	.byte 11, 15, 7
	.ascii " Welcome back, "
	.byte 10
	.ascii ". "
	.byte 11, 15, 4
	.ascii " A ship came into the harbor. "
	.byte 11, 15, 7
	.ascii " Be careful, & travel safely. "
	.byte 11, 15, 7
	.ascii " Thank you for everything. "
	.byte 11, 15, 4
	.ascii " I'm sure you'll be busy at home. "
	.byte 11, 15, 7
	.ascii " I'm sure new people will come, "
	.byte 11, 15, 7
	.ascii " looking for the elements & ore."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 11, 15, 4
	.ascii " I'll be an innkeeper here. "
	.byte 11, 15, 7
	.ascii " Mira agreed to give us the inn "
	.byte 11, 15, 7
	.ascii " I am very grateful to her. "
	.byte 11, 15, 4
	.ascii " I can't really complain. "
	.byte 11, 15, 7
	.ascii " From now on, I'll do my best "
	.byte 11, 15, 7
	.ascii " I have to work hard, for her."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 11, 30, 4
	.ascii " Well, I wish you well. "
	.byte 11, 15, 7, 17, 1
	.ascii " Please come visit sometime. "
	.byte 11, 15, 7
	.ascii " She will be happy to see you. "
	.byte 11, 15, 0, 0

.org 0x80159980
	addiu a0, a0, -0x7A2C
.org 0x801585D4
	.ascii "Alhena "
	.byte 7
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "Welcome back, "
	.byte 10
	.ascii "."
	.byte 11, 15, 7
	.ascii " You're a great guy, really."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 25, 4
	.ascii " Looks like the ship is waiting. "
	.byte 11, 15, 7
	.ascii " I wish you a safe voyage home. "
	.byte 11, 15, 7
	.ascii " When you get back, train. "
	.byte 11, 15, 4
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "I think I'll stay. "
	.byte 11, 30, 7
	.ascii " Wherever I go, my past "
	.byte 11, 15, 7
	.ascii " is inescapeable."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 25, 4
	.ascii " I must accept & overcome it, "
	.byte 11, 15, 7
	.ascii " I will ground myself, and live. "
	.byte 11, 15, 7
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "besides."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii " for some like me, "
	.byte 11, 15, 5
	.ascii " we're suckers for a challenge. "
	.byte 11, 15, 4
	.ascii " I'll never forget you, "
	.byte 10
	.ascii ". "
	.byte 11, 15, 7
	.ascii " Become a master"
	.byte 7
	.ascii " in the mainland."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 11, 15, 0, 0

.org 0x80159984
	addiu a0, a0, -0x7848
.org 0x801587B8
	.ascii "Bolko "
	.byte 7
	.ascii " You're back! Gahahaha! "
	.byte 11, 15, 15, 0, 228, 1, 7
	.ascii " That's my disciple! "
	.byte 11, 15, 17, 1, 4
	.ascii " You're going back home, huh? "
	.byte 11, 15, 7
	.ascii " Be careful on the way! "
	.byte 11, 15, 7
	.ascii " I have decided to stay in town. "
	.byte 11, 15, 4
	.ascii " It looks like Alhena is staying."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 25, 7
	.ascii " One day, you will understand."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 11, 15, 7
	.ascii " Just a matter of time."
	.byte 11, 15, 4
	.ascii " Well, that said,"
	.byte 11, 15, 7
	.ascii " I'm starting to feel better, "
	.byte 11, 15, 7
	.ascii " Thanks to you."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii ". things are good. "
	.byte 11, 15, 4
	.ascii " So long, and take care. "
	.byte 11, 15, 7
	.ascii " Remember me when"
	.byte 7
	.ascii " you get back home! "
	.byte 11, 15, 0, 0

.org 0x8015999C
	addiu a0, a0, -0x7694
.org 0x8015896C
	.ascii "Zania "
	.byte 7, 17, 0
	.ascii "Oh, I'm glad you're ok... After all, "
	.byte 11, 15, 7
	.ascii "You carried the element's shine. "
	.byte 11, 15, 4
	.ascii "The Elements entrust their light"
	.byte 11, 15, 7
	.ascii " & future to those who have a"
	.byte 11, 45, 7
	.ascii " strong body & will. That's you. "
	.byte 11, 15, 4
	.ascii " ...they say they're ready to sail. "
	.byte 11, 15, 7
	.ascii " I am sorry, I must say goodbye. "
	.byte 11, 45, 7
	.ascii " Thank you for saving my home. "
	.byte 11, 15, 4
	.ascii " I'll stay here till the end. "
	.byte 11, 15, 7
	.ascii " My memories & friends here. "
	.byte 11, 15, 7
	.ascii " You have been a great help. "
	.byte 11, 15, 4, 17, 1
	.ascii " Now for yourself, "
	.byte 11, 15, 7
	.ascii " live. "
	.byte 11, 15, 0, 0

.org 0x8015996C
	addiu a0, a0, -0x74E4
.org 0x80158B1C
	.ascii "Lesart"
	.byte 7
	.ascii " The boat is already here, look. "
	.byte 11, 15, 7
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "Thanks for everything. "
	.byte 11, 15, 4
	.ascii " As long as people live here, "
	.byte 11, 15, 7
	.ascii " I will be a lighthouse keeper."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 15, 7
	.ascii " That lighthouse guides many."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 25, 4
	.ascii " Now I will be one for this town. "
	.byte 11, 15, 7
	.ascii " I'm done despairing, but "
	.byte 11, 15, 7
	.ascii " You saved me."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 25, 4
	.ascii " Thank you, so much. "
	.byte 11, 15, 4
	.ascii " Are you leaving"
	.byte 7
	.ascii " already "
	.byte 10
	.ascii "? "
	.byte 11, 15, 5
	.ascii " You should say"
	.byte 7
	.ascii " goodbye to everyone, "
	.byte 11, 15, 7
	.ascii " Alright... "
	.byte 11, 15, 0, 0

.org 0x80159964
	addiu a0, a0, -0x735C
.org 0x80158CA4
	.ascii "Mira "
	.byte 7, 17, 0
	.ascii " Welcome back, I knew you'd"
	.byte 11, 15, 7
	.ascii " return. I predicted it. "
	.byte 11, 15, 4
	.ascii " The boat's coming soon, be safe. "
	.byte 11, 15, 7
	.ascii " I'll leave on the next boat too. "
	.byte 11, 15, 7
	.ascii " Headed to my husband's home. "
	.byte 11, 15, 4
	.ascii " To say goodbye to this inn, "
	.byte 11, 15, 7
	.ascii " It's painful, but."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii " for our kids, "
	.byte 11, 15, 7
	.ascii " It's a \"place with good air.\" "
	.byte 11, 15, 4, 17, 1
	.ascii " When I settle, I'll write you. "
	.byte 11, 15, 7
	.ascii " Please come visit"
	.byte 7
	.ascii " when you are free. "
	.byte 11, 15, 0, 0

.org 0x80159978
	addiu a0, a0, -0x71F0
.org 0x80158E10
	.ascii "Sadal "
	.byte 7
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "Welcome back. "
	.byte 11, 15, 7
	.ascii " Looks like it's finished."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 25, 4
	.ascii "You saved us."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii " maybe the world-"
	.byte 11, 15, 7
	.ascii " I never thought I'd see it."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 11, 15, 7
	.ascii "I'll have to live to see more."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii " Ha."
	.byte 11, 15, 4
	.ascii " Have a safe trip, "
	.byte 10
	.ascii ". "
	.byte 11, 30, 7
	.ascii " I think I'll stay & smith. "
	.byte 11, 15, 7
	.ascii " For those who remain in town. "
	.byte 11, 15, 4
	.ascii " I'll never forget you."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 11, 15, 7
	.ascii " Well then, take care. "
	.byte 11, 15, 0, 0

.org 0x8015997C
	addiu a0, a0, -0x7094
.org 0x80158F6C
	.ascii "Regulus "
	.byte 7
	.ascii " "
	.byte 10
	.ascii ", how are you? "
	.byte 11, 15, 7
	.ascii " Now the land will be at peace."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 25, 4
	.ascii " So you're off to the mainland."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 25, 7
	.ascii " I'll stay & protect the city "
	.byte 11, 15, 7
	.ascii " from the demon remnants."
	.byte 11, 15, 4
	.ascii " Tell stories of what happened, "
	.byte 11, 15, 7
	.ascii " For what you fought for."
	.byte 11, 15, 7
	.ascii " it's the least I can do. "
	.byte 11, 15, 4
	.ascii " If you ever return, "
	.byte 11, 15, 7
	.ascii " Come see me anytime. "
	.byte 11, 15, 7
	.ascii " Well, take care. "
	.byte 11, 15, 0, 0

.org 0x80159994
	addiu a0, a0, -0x6F44
.org 0x801590BC
	.ascii "Capella "
	.byte 7
	.ascii " I was too worried to cook. "
	.byte 11, 15, 7
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "Yeah, the port is ready to sail. "
	.byte 11, 15, 7
	.ascii " Yes, we're going home."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 25, 4
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "While at sea, be careful. "
	.byte 11, 15, 7
	.ascii " I pray you remember this place. "
	.byte 11, 45, 7
	.ascii " This city."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii ". and me."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 25, 0, 0

.org 0x80159968
	addiu a0, a0, -0x6E48
.org 0x801591B8
	.ascii "Alfeca "
	.byte 7, 17, 0
	.ascii " "
	.byte 10
	.ascii "! "
	.byte 11, 15, 7
	.ascii " I'm so glad you made it. "
	.byte 11, 15, 4
	.ascii " I received word aship has come. "
	.byte 11, 15, 7
	.ascii " We too will leave as soon. "
	.byte 11, 30, 7
	.ascii " I intend to return to home. "
	.byte 11, 15, 4
	.ascii " It's a rural, but good for kids. "
	.byte 11, 15, 7
	.ascii " The trouble Mira went through, "
	.byte 11, 15, 7
	.ascii " I think it'll get better for her."
	.byte 11, 15, 4, 17, 1
	.ascii " When you get settled,"
	.byte 7
	.ascii " please come visit us. "
	.byte 11, 15, 7
	.ascii " Take care then. "
	.byte 11, 15, 0, 0

.org 0x80159974
	addiu a0, a0, -0x6D00
.org 0x80159300
	.ascii "Sadal"
	.byte 7
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "Oh my"
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 25, 7
	.ascii " You're still just the same."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 25, 0, 0

.org 0x80159988
	addiu a0, a0, -0x6CB8
.org 0x80159348
	.ascii "Witch "
	.byte 7
	.ascii " Oh my, I'm sorry. "
	.byte 11, 15, 0, 0

.org 0x80159970
	addiu a0, a0, -0x6C7C
.org 0x80159384
	.ascii "Hamal"
	.byte 7
	.ascii " Oh, our hero! "
	.byte 11, 15, 7
	.ascii " I'm glad you're safe. "
	.byte 11, 15, 4
	.ascii " I can't work when you leave, so "
	.byte 11, 15, 7
	.ascii " I'm thinking of going as well. "
	.byte 11, 30, 7
	.ascii " 'trying to figure out where. "
	.byte 11, 15, 4
	.ascii " After another drink or so, "
	.byte 11, 15, 7
	.ascii " I'll head to the ship too. "
	.byte 11, 15, 0, 0

.org 0x80159998
	addiu a0, a0, -0x6BA0
.org 0x80159460
	.ascii "             THANK YOU!"
	.byte 11, 15, 7
	.ascii "            GREAT WORK!"
	.byte 11, 15, 7
	.ascii "          SEE YOU AGAIN!"
	.byte 11, 15, 7
	.ascii " "
	.byte 11, 15, 7
	.ascii "                F I  N      "
	.byte 11, 15, 7
	.ascii "          Till next time..."
	.byte 11, 15, 0, 0

.org 0x80159990
	addiu a0, a0, -0x6B0C
.org 0x801594F4
	.ascii "Capella "
	.byte 7
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 10
	.ascii "! Welcome back! "
	.byte 11, 15, 7
	.ascii " ...I'm glad you're safe. "
	.byte 11, 15, 0, 0

.close