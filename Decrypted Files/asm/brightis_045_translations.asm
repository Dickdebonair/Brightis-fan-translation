.psx
.open "OVR\045.bin", 0x80158138

.org 0x8015A558
	addiu a0, a0, -0x7EC8
.org 0x80158138
	.byte 16, 1, 110, 2, 16, 1, 111, 2, 16, 1, 112, 2, 16, 0, 178, 1, 19, 0, 178
	.ascii " Hey hey, nice sword. "
	.byte 11, 15, 7
	.ascii " With those glowbrand weapons, "
	.byte 11, 15, 7
	.ascii " You could build great stuff. "
	.byte 11, 15, 4
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "Oh, you don't have any ore? "
	.byte 11, 15, 7
	.ascii " Should be some in the mines. "
	.byte 11, 15, 7
	.ascii " Well, do your best to find it. "
	.byte 11, 15, 14, 3, 17, 1
	.ascii " How's it going, find any ore? "
	.byte 11, 15, 7
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "Oh well, don't lose heart. "
	.byte 11, 15, 14, 3, 17, 2, 19, 1, 104
	.ascii " Oooh, you have ore. "
	.byte 11, 15, 7
	.ascii " Yes, let me tell you about it. "
	.byte 11, 15, 7
	.ascii " 3 types: red, blue, & yellow. "
	.byte 11, 15, 4
	.ascii "Each ore has its own attributes, "
	.byte 11, 15, 7
	.ascii " Red is fire, blue is water. And "
	.byte 11, 15, 7
	.ascii " yellow is lightning. "
	.byte 11, 15, 4
	.ascii " With that ore & 'glowbrand,' "
	.byte 11, 15, 7
	.ascii " I can make you a weapon with "
	.byte 11, 15, 7
	.ascii " it's attributes. How about it? "
	.byte 11, 15, 4
	.ascii " Or you can try yourself? "
	.byte 11, 15, 7
	.ascii " Read the poster over there. "
	.byte 11, 15, 4
	.ascii " If you think you can, why not? "
	.byte 11, 15, 7
	.ascii " But, if you think you can't, "
	.byte 11, 15, 7
	.ascii " You should leave it to me."
	.byte 11, 15, 14, 3, 17, 3, 0, 0

.org 0x8015A554
	addiu a0, a0, -0x7BD0
.org 0x80158430
	.byte 17, 0, 16, 1, 108, 2, 16, 1, 107, 2, 16, 0, 179, 1, 19, 0, 179
	.ascii " Oooh, you have ore. "
	.byte 11, 15, 7
	.ascii " Yes, let me tell you about it. "
	.byte 11, 15, 7
	.ascii " There are 3 types of ore: red, blue, & yellow. "
	.byte 11, 15, 4
	.ascii " Each ore has its own attributes, "
	.byte 11, 15, 7
	.ascii " Red is fire, blue is water. And "
	.byte 11, 15, 7
	.ascii " yellow is lightning. "
	.byte 11, 15, 4
	.ascii " I can put it's attribute, as long as "
	.byte 11, 15, 7
	.ascii " the weapon isn't from the Balgean empire. "
	.byte 11, 15, 4, 17, 1
	.ascii " If you have the materials,"
	.byte 11, 15, 7
	.ascii " I could make you something"
	.byte 11, 15, 7
	.ascii " Very impressive."
	.byte 11, 15, 14, 3, 17, 2, 19, 1, 104
	.ascii " Oh! What's up with that glowbrand? "
	.byte 11, 15, 7
	.ascii " With that ore & glowbrand weapon, "
	.byte 11, 15, 7
	.ascii " I can make you a weapon with "
	.byte 11, 15, 7
	.ascii " it's attributes. How about it? "
	.byte 11, 15, 4
	.ascii " Or you can try to build it yourself? "
	.byte 11, 15, 7
	.ascii " Read the paper on the wall there. "
	.byte 11, 15, 4
	.ascii " If you think you can, why not try it? "
	.byte 11, 15, 7
	.ascii " But, if you think you'll make a mistake, "
	.byte 11, 15, 7
	.ascii " You should leave it to me."
	.byte 11, 15, 17, 3, 0, 0

.org 0x8015A548
	addiu a0, a0, -0x78E8
.org 0x80158718
	.ascii "Sadal"
	.byte 7, 16, 0, 107, 1, 17, 0, 19, 0, 107
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "Are you properly equipped? "
	.byte 11, 15, 7
	.ascii "If it's unused, it's just baggage. "
	.byte 11, 15, 4
	.ascii " Some weapons that have"
	.byte 11, 15, 7
	.ascii " what are called attributes... "
	.byte 11, 15, 7
	.ascii " There's fire, lightning, & water. "
	.byte 11, 15, 4
	.ascii " The demons fall under these"
	.byte 11, 15, 7
	.ascii " 4 Classifications. "
	.byte 11, 15, 7
	.ascii " Best to study this:"
	.byte 11, 15, 4
	.ascii " Fire > Ltng | Fire < water "
	.byte 11, 15, 7
	.ascii " Ltng. > water | Ltng. < fire. "
	.byte 11, 15, 7
	.ascii " Water > fire | Water < Ltng. "
	.byte 11, 15, 5
	.ascii " Non-attributes = none apply. "
	.byte 11, 15, 4
	.ascii " Well, in any case, "
	.byte 11, 15, 7
	.ascii " To exploit a weakness, "
	.byte 11, 15, 7
	.ascii " is the basis of warfare. "
	.byte 11, 15, 4, 17, 1
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "What can I do for you? "
	.byte 11, 15, 7
	.ascii " If you need anything, just ask. "
	.byte 11, 15, 12, 1, 0, 0

.org 0x8015A538
	addiu a0, a0, -0x76BC
.org 0x80158944
	.ascii "Lesart "
	.byte 7, 16, 0, 104, 1, 17, 0, 19, 0, 104
	.ascii " The lighthouse uses elements."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 11, 15, 7
	.ascii " It cut through any darkness."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 11, 15, 4
	.ascii " The lighthouse lost its light,"
	.byte 11, 15, 7
	.ascii " that damned tower"
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 15, 7
	.ascii "The light is a guide for the sea."
	.byte 11, 15, 4
	.ascii "Without light,"
	.byte 7
	.ascii " a ship is like a lost child..."
	.byte 11, 15, 7
	.ascii "Without light, no ship will come..."
	.byte 11, 15, 4, 17, 1
	.ascii " The light."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 7
	.ascii " If only we had the light..."
	.byte 11, 15, 0, 0

.org 0x8015A55C
	addiu a0, a0, -0x7554
.org 0x80158AAC
	.ascii "Witch "
	.byte 7, 16, 0, 11, 1, 17, 0, 19, 0, 111
	.ascii " Oh, is that."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 11, 15, 7
	.ascii " Wow, that's a global element! "
	.byte 11, 15, 4
	.ascii " It's totally different from the "
	.byte 11, 15, 7
	.ascii " key elements that we use. "
	.byte 11, 45, 7
	.ascii " I see, they were protecting it. "
	.byte 11, 15, 4
	.ascii " That global element-"
	.byte 11, 15, 7
	.ascii " It can store magic. "
	.byte 11, 15, 7
	.ascii " So, even you can use magic. "
	.byte 11, 15, 4
	.ascii " Cast it by opening the menu &"
	.byte 11, 15, 7
	.ascii " set the spell in the menu."
	.byte 11, 15, 7
	.ascii " Use the 'Magic & Items' button. "
	.byte 4
	.ascii " However, it consumes MP, "
	.byte 11, 15, 7
	.ascii " so don't overuse it."
	.byte 11, 15, 7
	.ascii " MP will recover over time. "
	.byte 11, 15, 4
	.ascii " What do you say, shall I? "
	.byte 11, 15, 7
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "Of course, for a small fee. "
	.byte 11, 15, 14, 2, 17, 1
	.ascii " Shall I slot some"
	.byte 7
	.ascii " magic for you? "
	.byte 11, 15, 17, 2, 12, 3, 0, 0

.org 0x8015A544
	addiu a0, a0, -0x7328
.org 0x80158CD8
	.ascii "Alfeca"
	.byte 7, 16, 0, 106, 1, 17, 0, 19, 0, 106
	.ascii " What the."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii " you..."
	.byte 11, 15, 7
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "Hmmm,"
	.byte 11, 30
	.ascii " what's that big element? "
	.byte 11, 15, 4
	.ascii " Hm, "
	.byte 11, 15
	.ascii "\"at the back of the ruins\"... "
	.byte 11, 15, 7
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii ".Hey, I won't say anything bad "
	.byte 11, 15, 7
	.ascii " but you should avoid the ruins. "
	.byte 11, 15, 4
	.ascii " It was trouble for you before. "
	.byte 11, 15, 7
	.ascii " ...The ruins of this land, "
	.byte 11, 15, 7
	.ascii " there under some kind of curse "
	.byte 11, 15, 5
	.ascii " or protection over them, right? "
	.byte 11, 15, 4
	.ascii " That's why the cataclysm and "
	.byte 11, 15, 7
	.ascii " a lot of demons show up there... "
	.byte 11, 15, 7
	.ascii " You should stay here & wait. "
	.byte 11, 15, 5, 17, 1
	.ascii " Well, be careful of the demons. "
	.byte 11, 15, 7
	.ascii " It's not worth your life. "
	.byte 11, 15, 7
	.ascii " I don't want any more victims..."
	.byte 11, 15, 0, 0

.org 0x8015A524
	addiu a0, a0, -0x7100
.org 0x80158F00
	.ascii " If you want to learn to fight, "
	.byte 11, 15, 7
	.ascii " The Warrior's Guide inside-"
	.byte 11, 15, 7
	.ascii " You might want to read it. "
	.byte 11, 15, 5
	.ascii " It might help you. "
	.byte 11, 15, 0, 0

.org 0x8015A550
	addiu a0, a0, -0x706C
.org 0x80158F94
	.byte 19, 0, 178, 19, 0, 179, 19, 1, 104
	.ascii " Oooh, you have ore. "
	.byte 11, 15, 7
	.ascii " Yes, let me tell you about it. "
	.byte 11, 15, 7
	.ascii " 3 types: red, blue, & yellow. "
	.byte 11, 15, 4
	.ascii "Each ore has its own attributes, "
	.byte 11, 15, 7
	.ascii " Red is fire, blue is water. And "
	.byte 11, 15, 7
	.ascii " yellow is lightning. "
	.byte 11, 15, 4
	.ascii " I can add attributes, as long as "
	.byte 11, 15, 7
	.ascii " it is not from the empire. "
	.byte 11, 15, 4
	.ascii " Wow! You've got one. "
	.byte 11, 15, 7
	.ascii " With that ore & 'glowbrand,' "
	.byte 11, 15, 7
	.ascii " I can make you a weapon with "
	.byte 11, 15, 7
	.ascii " it's attributes. How about it? "
	.byte 11, 15, 4
	.ascii " Or you can try yourself? "
	.byte 11, 15, 7
	.ascii " Read the poster over there. "
	.byte 11, 15, 4
	.ascii " If you can, why not try it? "
	.byte 11, 15, 7
	.ascii " But, if you think you can't, "
	.byte 11, 15, 7
	.ascii " You should leave it to me."
	.byte 11, 15, 0, 0

.org 0x8015A53C
	addiu a0, a0, -0x6E44
.org 0x801591BC
	.ascii "Mira"
	.byte 7, 16, 0, 161, 2, 16, 0, 105, 1, 17, 0, 19, 0, 105
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "I knew it was you, "
	.byte 10
	.ascii ". "
	.byte 11, 15, 7
	.ascii " By your footsteps. "
	.byte 11, 15
	.ascii "Hmmm. "
	.byte 11, 15, 4
	.ascii " Are you feeling better now? "
	.byte 11, 15, 7
	.ascii " Don't push yourself too hard. "
	.byte 11, 15, 7
	.ascii " Please rest, when you need to. "
	.byte 11, 15, 14, 3, 17, 1, 19, 0, 161
	.ascii " Oh, "
	.byte 11, 15, 10
	.ascii ". "
	.byte 11, 15, 7
	.ascii " Have you seen Hamal around? "
	.byte 11, 15, 4
	.ascii " He was going to the Falls,"
	.byte 11, 15, 7
	.ascii " north of town, & hasn't."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 11, 15, 7
	.ascii " I hope he's ok."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "   I'm worried. "
	.byte 11, 15, 4
	.ascii " Oh? "
	.byte 11, 45
	.ascii "That's a global element? "
	.byte 11, 15, 7
	.ascii " The Witch is keen on those. "
	.byte 11, 15, 7
	.ascii " Why not have her check it? "
	.byte 11, 15, 14, 3, 17, 2
	.ascii " Are you going to rest today? "
	.byte 11, 15, 17, 3, 0, 0

.org 0x8015A568
	addiu a0, a0, -0x6C60
.org 0x801593A0
	.ascii "Bolko"
	.byte 7, 16, 0, 102, 2, 16, 0, 111, 1, 17, 0, 14, 2, 17, 1, 19, 0, 102
	.ascii " Well, I heard there were"
	.byte 11, 15, 7
	.ascii " demons in the ruins because"
	.byte 11, 15, 4
	.ascii " of that global element "
	.byte 11, 15, 7
	.ascii " They must get magic from it."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 11, 15, 7
	.ascii " Once gone, maybe they'll leave. "
	.byte 11, 15, 4
	.ascii " Well, you're a real soldier now. "
	.byte 11, 30, 7
	.ascii " Looks like the squad got wiped."
	.byte 11, 15, 7
	.ascii " I'm sure they'll be no reward. "
	.byte 11, 15, 4
	.ascii " Oh, I see it's you. "
	.byte 11, 15, 7
	.ascii " Looks like you've got some SP. "
	.byte 11, 15, 7
	.ascii " Want to learn a few tricks? "
	.byte 11, 15, 14, 3, 17, 2
	.ascii " I'll teach you something. "
	.byte 11, 15, 7
	.ascii " Which TECH do you want? "
	.byte 11, 15, 14, 3, 17, 3, 19, 1, 98, 12, 4, 0, 0

.org 0x8015A584
	addiu a0, a0, -0x6A98
.org 0x80159568
	.ascii " Hey, let me see that ore. "
	.byte 11, 15, 7
	.ascii " This is... I'm pretty sure... "
	.byte 11, 15, 7
	.ascii " This ore is called \"pure metal\". "
	.byte 11, 15, 4
	.ascii " It's a very high quality ore. "
	.byte 11, 15, 7
	.ascii " If you forge with it, you"
	.byte 7
	.ascii " can make something amazing. "
	.byte 11, 15, 5
	.ascii " But the sword also must be"
	.byte 11, 15, 7
	.ascii " of a very high purity. "
	.byte 11, 15, 4
	.ascii " There are two types,"
	.byte 11, 15, 7
	.ascii " white & black ore,"
	.byte 11, 15, 7
	.ascii " I'm not sure of their details. "
	.byte 11, 15, 5
	.ascii "Well, either would be great, but "
	.byte 11, 15, 7
	.ascii " You've gotta choose one to use. "
	.byte 11, 15, 7
	.ascii " But without a sword... "
	.byte 11, 15, 0, 0

.org 0x8015A530
	addiu a0, a0, -0x68E8
.org 0x80159718
	.ascii "Regulus"
	.byte 7, 16, 0, 103, 1, 17, 0, 19, 1, 99, 19, 0, 103
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "Well,"
	.byte 11, 30
	.ascii " no one was there..."
	.byte 11, 15, 7
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "I guess I was too late..."
	.byte 11, 15, 4
	.ascii " Some others were headed there. "
	.byte 11, 15, 7
	.ascii " Maybe they're still alive."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 11, 15, 7
	.ascii " If you find them, please help. "
	.byte 11, 15, 14, 2, 17, 1
	.ascii " Have you come to learn? "
	.byte 11, 15, 7
	.ascii " Which TECH shall we practice? "
	.byte 11, 15, 17, 2, 12, 6, 0, 0

.org 0x8015A560
	addiu a0, a0, -0x67D4
.org 0x8015982C
	.ascii "Regulus"
	.byte 7, 16, 0, 103, 1, 17, 0, 19, 1, 99, 19, 0, 103
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "Well, "
	.byte 11, 30
	.ascii " no one was there..."
	.byte 11, 15, 7
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "I guess I was too late..."
	.byte 11, 15, 4
	.ascii " But some others headed there. "
	.byte 11, 15, 7
	.ascii " Maybe they're still alive."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 11, 15, 7
	.ascii " If you find them, please help. "
	.byte 11, 15, 14, 2, 17, 1
	.ascii " Have you come to learn? "
	.byte 11, 15, 7
	.ascii " Which TECH do you want? "
	.byte 11, 15, 17, 2, 12, 6, 0, 0

.org 0x8015A564
	addiu a0, a0, -0x66C4
.org 0x8015993C
	.ascii "Alhena"
	.byte 7, 16, 0, 101, 1, 17, 0, 19, 1, 97, 19, 0, 101
	.ascii " There were only demons."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "?"
	.byte 11, 15, 7
	.ascii " ...everyone."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii " just..."
	.byte 11, 15, 4
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "Hey, you want to learn? "
	.byte 11, 15, 7
	.ascii " I need a distraction. "
	.byte 11, 15, 7
	.ascii " "
	.byte 10
	.ascii ", how about it? "
	.byte 11, 15, 14, 2, 17, 1
	.ascii " Have you come to learn the sword dance? "
	.byte 11, 15, 7
	.ascii " Which technique do you want to learn? "
	.byte 11, 15, 17, 2, 12, 5, 0, 0

.org 0x8015A534
	addiu a0, a0, -0x65B8
.org 0x80159A48
	.ascii "Alhena"
	.byte 7, 16, 0, 101, 1, 17, 0, 19, 1, 97, 19, 0, 101
	.ascii " There were only demons."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "? "
	.byte 11, 15, 7
	.ascii " ...everyone."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii " just..."
	.byte 11, 15, 4
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "Hey, you want to learn TECH? "
	.byte 11, 15, 7
	.ascii " I need a distraction. "
	.byte 11, 15, 7
	.ascii " "
	.byte 10
	.ascii ", how about it? "
	.byte 11, 15, 14, 2, 17, 1
	.ascii " Here to learn a sword dance? "
	.byte 11, 15, 7
	.ascii " Which TECH do you want? "
	.byte 11, 15, 17, 2, 12, 5, 0, 0

.org 0x8015A540
	addiu a0, a0, -0x64C0
.org 0x80159B40
	.ascii "Alfeca "
	.byte 7
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "hum, hummm~."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "hum~"
	.byte 11, 15, 0, 0

.org 0x8015A52C
	addiu a0, a0, -0x6490
.org 0x80159B70
	.ascii " Oh yes, let me tell you- "
	.byte 11, 15, 7
	.ascii " To learn swordsmanship fast"
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 15, 7
	.ascii " Uh."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "um"
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "what was it?"
	.byte 11, 15, 4
	.ascii " Right, in Regulus' dorms-"
	.byte 11, 15, 7
	.ascii " Read the warrior's guide. "
	.byte 11, 15, 7
	.ascii " I'm sure you'll find the details. "
	.byte 11, 15, 0, 0

.org 0x8015A528
	addiu a0, a0, -0x63C0
.org 0x80159C40
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "I see, I think I get it."
	.byte 11, 15, 7
	.ascii " That's why you survived. "
	.byte 11, 15, 7
	.ascii " You have clear foresight"
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 15, 4
	.ascii " In Regulus' dorms, "
	.byte 11, 15, 7
	.ascii " Read the Warrior's Guide. "
	.byte 11, 15, 7
	.ascii " It will help you. "
	.byte 11, 15, 0, 0

.org 0x8015A578
	addiu a0, a0, -0x630C
.org 0x80159CF4
	.ascii "~How to Play Cards~ "
	.byte 11, 15, 7
	.ascii "A popular game from the empire, "
	.byte 11, 15, 7
	.ascii " Here describes how to play. "
	.byte 11, 15, 0, 0

.org 0x8015A54C
	addiu a0, a0, -0x62B0
.org 0x80159D50
	.ascii "Sadal"
	.byte 7
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "if you need something,"
	.byte 11, 15, 7
	.ascii "  Speak to me over the counter. "
	.byte 11, 15, 0, 0

.org 0x8015A588
	addiu a0, a0, -0x6260
.org 0x80159DA0
	.ascii "DANGER! "
	.byte 7
	.ascii "　　Below, is the abandoned mine. "
	.byte 7
	.ascii "　　Demons may appear. "
	.byte 0, 0

.org 0x8015A580
	addiu a0, a0, -0x6218
.org 0x80159DE8
	.ascii "~The Warrior's Guide~"
	.byte 11, 15, 7
	.ascii " It says:"
	.byte 7
	.ascii " How to improve your skills. "
	.byte 11, 15, 0, 0

.org 0x8015A57C
	addiu a0, a0, -0x61D4
.org 0x80159E2C
	.ascii "~Notes on blacksmithing~"
	.byte 11, 15, 7
	.ascii " It describes how to blacksmith. "
	.byte 11, 15, 0, 0

.org 0x8015A56C
	addiu a0, a0, -0x6194
.org 0x80159E6C
	.ascii "Capella "
	.byte 7
	.ascii ". Whoa, you're back, "
	.byte 10
	.ascii ". "
	.byte 11, 15, 7
	.ascii " Safe and sound too. "
	.byte 11, 15, 0, 0

.close