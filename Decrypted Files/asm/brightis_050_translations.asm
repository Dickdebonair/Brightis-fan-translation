.psx
.open "OVR\050.bin", 0x80158138

.org 0x8015B12C
	addiu a0, a0, -0x7EC8
.org 0x80158138
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
	.ascii " With that ore, "
	.byte 11, 15, 7
	.ascii " I can attribute a weapon, "
	.byte 11, 15, 7
	.ascii "long as it's not from the empire. "
	.byte 11, 15, 4
	.ascii " Whoa! You've got one. "
	.byte 11, 15, 7
	.ascii " With a glowbrand weapon & ore, "
	.byte 11, 15, 7
	.ascii " I can build a new one. Well? "
	.byte 11, 15, 4
	.ascii " Or you can try yourself? "
	.byte 11, 15, 7
	.ascii " Read the poster over there. "
	.byte 11, 15, 4
	.ascii " If you think you can, try it. "
	.byte 11, 15, 7
	.ascii " But, if you think you can't- "
	.byte 11, 15, 7
	.ascii " You should leave it to me. "
	.byte 11, 15, 0, 0

.org 0x8015B134
	addiu a0, a0, -0x7CA8
.org 0x80158358
	.byte 16, 1, 110, 2, 16, 1, 111, 2, 16, 1, 112, 2, 16, 0, 178, 1, 19, 0, 178
	.ascii " Hey hey, nice sword. "
	.byte 11, 15, 7
	.ascii " With those glowbrand weapons, "
	.byte 11, 15, 7
	.ascii " I could build awesome gear. "
	.byte 11, 15, 4
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "Oh, you don't have any ore? "
	.byte 11, 15, 7
	.ascii " You should check the mines. "
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
	.ascii " Each ore has its own attributes, "
	.byte 11, 15, 7
	.ascii " Red is fire, blue is water. And "
	.byte 11, 15, 7
	.ascii " yellow is lightning. "
	.byte 11, 15, 4
	.ascii " With that ore & glowbrand, "
	.byte 11, 15, 7
	.ascii " I can make you a weapon with "
	.byte 11, 15, 7
	.ascii " it's attributes. How about it? "
	.byte 11, 15, 4
	.ascii " Or you can try yourself? "
	.byte 11, 15, 7
	.ascii " Read the poster over there. "
	.byte 11, 15, 4
	.ascii " If you think you can, try it "
	.byte 11, 15, 7
	.ascii " But, if you think you can't, "
	.byte 11, 15, 7
	.ascii " You should leave it to me."
	.byte 11, 15, 14, 3, 17, 3, 0, 0

.org 0x8015B130
	addiu a0, a0, -0x79B4
.org 0x8015864C
	.byte 17, 0, 16, 1, 108, 2, 16, 1, 107, 2, 16, 0, 179, 1, 19, 0, 179
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
	.ascii " With that ore, "
	.byte 11, 15, 7
	.ascii " I can attribute to a weapon, "
	.byte 11, 15, 7
	.ascii "long as it's not from the empire. "
	.byte 11, 15, 4, 17, 1
	.ascii " If you have the materials, "
	.byte 11, 15, 7
	.ascii " I could make you "
	.byte 11, 15, 7
	.ascii " something amazing. "
	.byte 11, 15, 14, 3, 17, 2, 19, 1, 104
	.ascii " Oh! Is that glowbrand sword? "
	.byte 11, 15, 7
	.ascii " With a glowbrand weapon & ore, "
	.byte 11, 15, 7
	.ascii " I can build you one. Well? "
	.byte 11, 15, 4
	.ascii " Or you can try yourself? "
	.byte 11, 15, 7
	.ascii " Read the poster over there. "
	.byte 11, 15, 4
	.ascii " If you think you can, try it "
	.byte 11, 15, 7
	.ascii " But, if you think you can't-"
	.byte 11, 15, 7
	.ascii " You should leave it to me. "
	.byte 11, 15, 17, 3, 0, 0

.org 0x8015B11C
	addiu a0, a0, -0x7734
.org 0x801588CC
	.ascii "Alfeca"
	.byte 7, 16, 0, 106, 1, 17, 0, 19, 0, 106
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "Oh, "
	.byte 10
	.ascii ". "
	.byte 11, 15, 7
	.ascii " Would you hear me out. "
	.byte 11, 15, 4
	.ascii " 2 years ago, as part of the 1st,"
	.byte 11, 15, 7
	.ascii " I came to this island. "
	.byte 11, 15, 5
	.ascii " But, we were met by demons."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 15, 7
	.ascii " I abandoned my companions and "
	.byte 11, 15, 7
	.ascii " I ran for my life. "
	.byte 11, 15, 4
	.ascii " I lost the demons, and "
	.byte 11, 15, 7
	.ascii " I managed to reach this town"
	.byte 11, 15, 7
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "but I was naive. "
	.byte 11, 15, 4
	.ascii " I lost them, but others-"
	.byte 11, 15, 7
	.ascii " The demons attacked a woman. "
	.byte 11, 15, 7
	.ascii " I ran over & chased them away. "
	.byte 11, 15, 4
	.ascii " But I couldn't save her, or Mira. "
	.byte 11, 15, 7
	.ascii " 'cause of me, she was injured."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 15, 7
	.ascii " If only I hadn't run here."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 11, 15, 14, 2, 17, 1
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "Please, save us"
	.byte 11, 60, 7
	.ascii " This town."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "No, this continent."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 11, 15, 17, 2, 0, 0

.org 0x8015B0F4
	addiu a0, a0, -0x74D8
.org 0x80158B28
	.ascii "Regulus "
	.byte 7, 16, 0, 103, 1, 17, 0, 19, 1, 99, 19, 0, 103
	.ascii " Techniques are done by inputs,"
	.byte 11, 15, 7
	.ascii " You're not bad at inputs, right? "
	.byte 11, 15, 4
	.ascii " So, you need to put TECHs on"
	.byte 11, 15, 7
	.ascii " cmds. you're comfortable with."
	.byte 11, 15, 7
	.ascii " It's good to swap them around. "
	.byte 11, 15, 4, 17, 1
	.ascii " "
	.byte 10
	.ascii ", I'll teach you TECH. "
	.byte 11, 15, 7
	.ascii " Which would you like to learn? "
	.byte 11, 15, 12, 6, 0, 0

.org 0x8015B108
	addiu a0, a0, -0x73C4
.org 0x80158C3C
	.ascii "Hamal"
	.byte 7, 16, 0, 109, 1, 17, 0, 19, 0, 109
	.ascii " It was scary, but I'm here. "
	.byte 11, 15, 7
	.ascii " I just came from the Snowfield. "
	.byte 11, 15, 4
	.ascii " I went to get snow orchids, "
	.byte 11, 15, 7
	.ascii " When I crossed the Valley,"
	.byte 11, 15, 7
	.ascii " The atmosphere always changes. "
	.byte 11, 15, 4
	.ascii " Many demons gathered there, "
	.byte 11, 15, 7
	.ascii " so many in fact, that  "
	.byte 11, 15, 7
	.ascii " I got scared and ran away. "
	.byte 11, 15, 4, 16, 0, 160, 2
	.ascii " Well, that aside."
	.byte 11, 15, 7
	.ascii " Do you want to buy something? "
	.byte 11, 15, 14, 3, 17, 1
	.ascii " Oh, forgive me. "
	.byte 11, 15, 7
	.ascii " What can I get for you? "
	.byte 11, 15, 14, 3, 17, 2
	.ascii " The herb made from snow orchids, "
	.byte 11, 15, 7
	.ascii " Lypen herb, are sold out. "
	.byte 11, 15, 7
	.ascii " Sorry, "
	.byte 10
	.ascii ". "
	.byte 11, 15, 17, 3, 12, 2, 0, 0

.org 0x8015B17C
	addiu a0, a0, -0x71EC
.org 0x80158E14
	.ascii "Rifula"
	.byte 7
	.ascii " Sorry, big bro. "
	.byte 11, 15, 0, 0

.org 0x8015B0FC
	addiu a0, a0, -0x71D0
.org 0x80158E30
	.ascii "Bolko"
	.byte 7, 16, 0, 102, 1, 17, 0, 19, 1, 98, 19, 0, 102
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "What is it, "
	.byte 10
	.ascii "?"
	.byte 11, 15, 7
	.ascii " Hhmph."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 11, 10, 11, 15, 4
	.ascii " Hey, "
	.byte 11, 15
	.ascii " Alhena's still... "
	.byte 11, 15, 7
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "Oh, it's nothing. "
	.byte 11, 30, 7
	.ascii " Sigh."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 11, 15, 4, 17, 1
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "Ah, technique."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 11, 15, 7
	.ascii " Which do you want to learn? "
	.byte 11, 15, 17, 2, 12, 4, 0, 0

.org 0x8015B154
	addiu a0, a0, -0x70F8
.org 0x80158F08
	.ascii "Witch "
	.byte 7, 17, 0, 19, 0, 111
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
	.ascii " I see, the demons protected it. "
	.byte 11, 15, 4
	.ascii " That global element-"
	.byte 11, 15, 7
	.ascii " It can store magic. "
	.byte 11, 15, 7
	.ascii " So, even you can use magic. "
	.byte 11, 15, 4
	.ascii " You cast by opening the menu &"
	.byte 11, 15, 7
	.ascii " set the spell in the menu & "
	.byte 11, 15, 7
	.ascii " use the \"Magic & Items\" button. "
	.byte 4
	.ascii " However, it consumes MP, "
	.byte 11, 15, 7
	.ascii " so don't overuse it. Your MP"
	.byte 11, 15, 7
	.ascii " will recover overtime. "
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
	.byte 11, 15, 12, 3, 0, 0

.org 0x8015B10C
	addiu a0, a0, -0x6EF4
.org 0x8015910C
	.byte 16, 0, 170, 1, 17, 0, 19, 0, 170
	.ascii " Is that you, "
	.byte 10
	.ascii "? "
	.byte 11, 30, 7
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 20
	.ascii "That's nightshade fruit... "
	.byte 11, 45, 4
	.ascii " Nightshade is very rare & "
	.byte 11, 15, 7
	.ascii " only grows with magic light."
	.byte 11, 15, 7
	.ascii " With it, we can make medicines. "
	.byte 11, 15, 4
	.ascii " Wonderful, strong medicines "
	.byte 11, 15, 7
	.ascii " that can help many people. "
	.byte 11, 15, 7
	.ascii " Please, "
	.byte 11, 15
	.ascii "bring me that fruit! "
	.byte 11, 15, 14, 2, 17, 1
	.ascii " Um, "
	.byte 10
	.ascii ". "
	.byte 11, 30, 7
	.ascii " About the other day."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 11, 15, 7
	.ascii " Can you bring me nightshade? "
	.byte 11, 15, 4
	.ascii " With them, we can make many "
	.byte 11, 15, 7
	.ascii " wonderful medicines. "
	.byte 11, 15, 7
	.ascii "I know it's a lot, but consider it. "
	.byte 11, 15, 17, 2, 0, 0

.org 0x8015B138
	addiu a0, a0, -0x6D38
.org 0x801592C8
	.ascii " Hey, let me see that ore. "
	.byte 11, 15, 7
	.ascii " This is... I'm pretty sure... "
	.byte 11, 15, 7
	.ascii " This ore is called \"pure metal\". "
	.byte 11, 15, 4
	.ascii " It's a very high quality ore. "
	.byte 11, 15, 7
	.ascii " If you forge with it,"
	.byte 7
	.ascii " you can make amazing things. "
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
	.ascii " Well, either would be great, but "
	.byte 11, 15, 7
	.ascii " You gotta choose which one. "
	.byte 11, 15, 7
	.ascii " But without a sword... "
	.byte 11, 15, 0, 0

.org 0x8015B170
	addiu a0, a0, -0x6B8C
.org 0x80159474
	.ascii "Zania"
	.byte 7, 16, 0, 163, 1, 17, 0, 19, 0, 163
	.ascii " Oh, "
	.byte 10
	.ascii ". "
	.byte 11, 15, 7
	.ascii " There are many secrets here. "
	.byte 11, 30, 7
	.ascii " Shall I tell you about them? "
	.byte 11, 15, 4
	.ascii " Cape Nasheera, a powerful"
	.byte 11, 10, 11, 10, 11, 15, 7
	.ascii " family's tomb is there. "
	.byte 11, 15, 5
	.ascii " But to protect from thieves, "
	.byte 11, 15, 7
	.ascii " powerful magic has been cast. "
	.byte 11, 15, 4
	.ascii " But don't be afraid & panic. "
	.byte 11, 15, 7
	.ascii " If you are not a grave robber,"
	.byte 11, 15, 7
	.ascii " the stones will surely help you. "
	.byte 11, 15, 4, 17, 1
	.ascii " I hope you will succeed. "
	.byte 11, 15, 7
	.ascii " As always, do your best. "
	.byte 11, 15, 0, 0

.org 0x8015B0E0
	addiu a0, a0, -0x6A08
.org 0x801595F8
	.ascii "Zania "
	.byte 7, 16, 0, 163, 1, 17, 0, 19, 0, 163
	.ascii " Oh, "
	.byte 10
	.ascii ". "
	.byte 11, 15, 7
	.ascii " There are many secrets here."
	.byte 11, 30, 7
	.ascii " Shall I tell you about one? "
	.byte 11, 15, 4
	.ascii " Cape Nasira."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 15, 7
	.ascii " There is an ancient tomb there. "
	.byte 11, 15, 5
	.ascii " To protect it from thieves, "
	.byte 11, 15, 7
	.ascii " powerful magic is cast on it. "
	.byte 11, 15, 4
	.ascii " But don't be afraid & panic. "
	.byte 11, 15, 7
	.ascii " If you're not a thief, the"
	.byte 11, 15, 7
	.ascii " stones will surely help you. "
	.byte 11, 15, 4, 17, 1
	.ascii " Please fulfil the mission."
	.byte 11, 15, 7
	.ascii " and, please do your best. "
	.byte 11, 15, 0, 0

.org 0x8015B124
	addiu a0, a0, -0x6890
.org 0x80159770
	.ascii "Sadal"
	.byte 7, 16, 0, 107, 1, 17, 0, 19, 0, 107
	.ascii " Oh, "
	.byte 10
	.ascii ". "
	.byte 11, 15, 7
	.ascii " How's your weapon? "
	.byte 11, 15, 4
	.ascii "Well, do you know about weapons "
	.byte 11, 15, 7
	.ascii " that can power key elements? "
	.byte 11, 15, 7
	.ascii " It's called a \"Rayblade\"... "
	.byte 11, 15, 4
	.ascii " When equipped, the key element "
	.byte 11, 15, 7
	.ascii " is said to shine longer. "
	.byte 11, 15, 7
	.ascii " There's also an opposite blade. "
	.byte 11, 15, 4, 17, 1
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "What can I do for you? "
	.byte 11, 15, 7
	.ascii " If anything, feel free to ask. "
	.byte 11, 15, 17, 2, 12, 1, 0, 0

.org 0x8015B15C
	addiu a0, a0, -0x6748
.org 0x801598B8
	.ascii "Regulus"
	.byte 7, 16, 0, 103, 1, 17, 0, 19, 1, 99, 19, 0, 103
	.ascii " Techniques are done by input."
	.byte 11, 15, 7
	.ascii " Some are better with commands. "
	.byte 11, 15, 4
	.ascii " So, I put the most used TECH, "
	.byte 11, 15, 7
	.ascii " on commands most comfortable."
	.byte 11, 15, 7
	.ascii " I would suggest you set yours. "
	.byte 11, 15, 4, 17, 1
	.ascii " "
	.byte 10
	.ascii ", I'll teach you TECH. "
	.byte 11, 15, 7
	.ascii " Which would you like to learn? "
	.byte 11, 15, 12, 6, 0, 0

.org 0x8015B14C
	addiu a0, a0, -0x6644
.org 0x801599BC
	.ascii "Witch "
	.byte 7, 19, 0, 167, 19, 1, 29
	.ascii " Oh, that's."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 30, 7
	.ascii " Hey, let me see that book. "
	.byte 11, 15, 4
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 11, 10
	.ascii "Hmm, I see."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 15, 7
	.ascii " It explains a lot"
	.byte 11, 15, 7
	.ascii " of things on magic. "
	.byte 11, 15, 4
	.ascii " This book is well researched."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 11, 30, 7
	.ascii " Hey, can you come back later? "
	.byte 11, 15, 7
	.ascii "I'll slot better magic than ever."
	.byte 11, 15, 0, 0

.org 0x8015B0F8
	addiu a0, a0, -0x6544
.org 0x80159ABC
	.ascii "Alhena"
	.byte 7, 16, 0, 101, 1, 17, 0, 19, 1, 97, 19, 0, 101
	.ascii " Sword Dances are timed."
	.byte 11, 15, 7
	.ascii " If you press Attack & TECH,"
	.byte 11, 15, 7
	.ascii " It will always appear. "
	.byte 11, 15, 4
	.ascii " If you aim the combo well, "
	.byte 11, 15, 7
	.ascii " you can win without"
	.byte 7
	.ascii " taking damage. "
	.byte 11, 15, 14, 2, 17, 1
	.ascii " Here to learn sword dances? "
	.byte 11, 15, 7
	.ascii " Which technique do you want? "
	.byte 11, 15, 17, 2, 12, 5, 0, 0

.org 0x8015B150
	addiu a0, a0, -0x644C
.org 0x80159BB4
	.ascii "Witch "
	.byte 7, 19, 0, 168, 19, 1, 30
	.ascii " Oh, that."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 30, 7
	.ascii " Hey, lend me that book. "
	.byte 11, 15, 4
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "Oh, my gosh! "
	.byte 11, 15, 7
	.ascii " This has a lot about"
	.byte 11, 15, 7
	.ascii " ancient forbidden magic. "
	.byte 11, 15, 4
	.ascii " This book is amazing."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 11, 30, 7
	.ascii " Hey, can you come back later? "
	.byte 11, 15, 7
	.ascii " I'll have tremendous magic. "
	.byte 11, 15, 0, 0

.org 0x8015B168
	addiu a0, a0, -0x635C
.org 0x80159CA4
	.ascii "Capella "
	.byte 7, 16, 0, 110, 1, 17, 0, 19, 0, 110
	.ascii " Ah, "
	.byte 10
	.ascii ". "
	.byte 11, 15, 7
	.ascii " Hey, let me ask you something."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 11, 15, 4
	.ascii " Something's with the animals. "
	.byte 11, 15, 7
	.ascii " They seem to be scared. "
	.byte 11, 15, 7
	.ascii " Do you know anything about it? "
	.byte 11, 15, 14, 2, 17, 1
	.ascii " \"Not at all\"."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 11, 15, 7
	.ascii " What's going on."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "? "
	.byte 11, 15, 17, 2, 0, 0

.org 0x8015B164
	addiu a0, a0, -0x6274
.org 0x80159D8C
	.ascii "Bolko"
	.byte 7, 16, 0, 102, 1, 17, 0, 19, 1, 98, 19, 0, 102
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "what's up, "
	.byte 10
	.ascii "? "
	.byte 11, 15, 7
	.ascii " Man."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 11, 15, 4
	.ascii " Hey, "
	.byte 11, 15
	.ascii " isn't Alhena so nice? "
	.byte 11, 15, 7
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "Oh, nevermind. "
	.byte 11, 30, 7
	.ascii " But man."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 11, 15, 4, 17, 1
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "Right, Techniques."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii ". "
	.byte 11, 10, 11, 15, 7
	.ascii " Which do you want to learn? "
	.byte 11, 15, 12, 4, 0, 0

.org 0x8015B0F0
	addiu a0, a0, -0x6198
.org 0x80159E68
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
	.ascii " what was it?"
	.byte 11, 15, 4
	.ascii " Right, in Regulus' dorms-"
	.byte 11, 15, 7
	.ascii " Read the warrior's guide. "
	.byte 11, 15, 7
	.ascii " I'm sure you'll find the details. "
	.byte 11, 15, 0, 0

.org 0x8015B118
	addiu a0, a0, -0x60C8
.org 0x80159F38
	.ascii " Hi, "
	.byte 10
	.ascii ". "
	.byte 11, 15, 7
	.ascii " I'm usually in this bar, so "
	.byte 11, 15, 7
	.ascii " If you need anything, come by. "
	.byte 11, 15, 4
	.ascii " Let's have a drink & play cards. "
	.byte 11, 15, 7
	.ascii " There's a poster on how to play "
	.byte 11, 15, 7
	.ascii " Read it in your spare time. "
	.byte 11, 15, 0, 0

.org 0x8015B160
	addiu a0, a0, -0x6010
.org 0x80159FF0
	.ascii "Alhena"
	.byte 7, 16, 0, 101, 1, 17, 0, 19, 1, 97, 19, 0, 101
	.ascii " Sword Dances are timed "
	.byte 11, 15, 7
	.ascii " If you aim the combo well, "
	.byte 11, 15, 7
	.ascii " you can win without damage. "
	.byte 11, 15, 14, 2, 17, 1
	.ascii " Here to learn a sword dance? "
	.byte 11, 15, 7
	.ascii " Which do you want to learn? "
	.byte 11, 15, 17, 2, 12, 5, 0, 0

.org 0x8015B144
	addiu a0, a0, -0x5F58
.org 0x8015A0A8
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "white? ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "It's snowing. "
	.byte 11, 30, 7
	.ascii " The mtn."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii " that castle."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii " what!? "
	.byte 11, 45, 7
	.ascii " The castle's magic is growing! "
	.byte 11, 15, 0, 0

.org 0x8015B0EC
	addiu a0, a0, -0x5ED8
.org 0x8015A128
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
	.ascii " In Regulus' dorm, "
	.byte 11, 15, 7
	.ascii " Read the Warrior's Guide. "
	.byte 11, 15, 7
	.ascii " It will help you. "
	.byte 11, 15, 0, 0

.org 0x8015B140
	addiu a0, a0, -0x5E24
.org 0x8015A1DC
	.ascii "Witch "
	.byte 7, 16, 12, 14, 1, 17, 0
	.ascii " Oh, "
	.byte 10
	.ascii ". "
	.byte 11, 15, 7
	.ascii " Another global element-"
	.byte 11, 15, 7
	.ascii " You got it. "
	.byte 11, 15, 4, 17, 1, 19, 0, 108
	.ascii " 4 Global elements... "
	.byte 11, 15, 7
	.ascii " Finally, one more to go. "
	.byte 11, 15, 7
	.ascii " I'll learn where the last is. "
	.byte 11, 15, 0, 0

.org 0x8015B148
	addiu a0, a0, -0x5D80
.org 0x8015A280
	.ascii " Something bad is coming! "
	.byte 11, 15, 7
	.ascii " What they're up to!?"
	.byte 11, 15, 7
	.ascii " "
	.byte 10
	.ascii ", hurry! "
	.byte 11, 15, 4
	.ascii " If we don't get the "
	.byte 11, 15, 7
	.ascii " Dark Element from them, "
	.byte 11, 15, 7
	.ascii " Something terrible will happen! "
	.byte 11, 15, 12, 3, 0, 0

.org 0x8015B178
	addiu a0, a0, -0x5CE0
.org 0x8015A320
	.ascii "Acre"
	.byte 7, 16, 0, 172, 1, 17, 0
	.ascii " Hm."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii " Have you seen Rifula?"
	.byte 11, 15, 7
	.ascii " I haven't seen him in a while. "
	.byte 11, 15, 14, 2, 17, 1, 20, 0, 171
	.ascii " I heard that you saved my boy, "
	.byte 11, 15, 7
	.ascii " Thank you so much. "
	.byte 11, 15, 17, 2, 0, 0

.org 0x8015B16C
	addiu a0, a0, -0x5C48
.org 0x8015A3B8
	.ascii " Oh, yeah. I heard you beat "
	.byte 11, 15, 7
	.ascii " Hamal in a card game."
	.byte 11, 15, 5
	.ascii " I'm pretty strong, if I do say. "
	.byte 11, 15, 7
	.ascii " Next time, you can play me. "
	.byte 11, 15, 0, 0

.org 0x8015B0E8
	addiu a0, a0, -0x5BC8
.org 0x8015A438
	.ascii " If you want to learn to fight, "
	.byte 11, 15, 7
	.ascii " The Warrior's Guide inside, "
	.byte 11, 15, 7
	.ascii " You might want to read it. "
	.byte 11, 15, 5
	.ascii " It might help you. "
	.byte 11, 15, 0, 0

.org 0x8015B100
	addiu a0, a0, -0x5B4C
.org 0x8015A4B4
	.ascii "Lesart "
	.byte 7, 16, 0, 104, 1, 17, 0, 19, 0, 104
	.ascii " If you intensify light,"
	.byte 7
	.ascii " it creates deeper darkness."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 11, 15, 7, 17, 1
	.ascii " Do not only rely on the light. "
	.byte 11, 15, 0, 0

.org 0x8015B110
	addiu a0, a0, -0x5AD4
.org 0x8015A52C
	.ascii " Oh! Thank you!!! Now we"
	.byte 11, 15, 7
	.ascii " can make stronger medicine! "
	.byte 11, 30, 7
	.ascii " Please come back in a while. "
	.byte 11, 15, 0, 0

.org 0x8015B188
	addiu a0, a0, -0x5A74
.org 0x8015A58C
	.ascii "~How to Play Cards~ "
	.byte 11, 15, 7
	.ascii "A popular game from the empire, "
	.byte 11, 15, 7
	.ascii " Here describes how to play. "
	.byte 11, 15, 0, 0

.org 0x8015B13C
	addiu a0, a0, -0x5A18
.org 0x8015A5E8
	.ascii "Witch "
	.byte 7
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "Who is it? I'm a little busy."
	.byte 11, 15, 7
	.ascii " If you need me, come upstairs? "
	.byte 11, 15, 0, 0

.org 0x8015B114
	addiu a0, a0, -0x59C0
.org 0x8015A640
	.byte 19, 0, 173
	.ascii " I see."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 15, 7
	.ascii " I'm sorry, but I can't help it. "
	.byte 11, 15, 7
	.ascii " Let's get back to work. "
	.byte 11, 15, 0, 0

.org 0x8015B120
	addiu a0, a0, -0x596C
.org 0x8015A694
	.ascii " ...next time you settle in, "
	.byte 11, 15, 7
	.ascii " how about a drink & cards? "
	.byte 11, 15, 7
	.ascii " Haha, my treat. "
	.byte 11, 15, 0, 0

.org 0x8015B174
	addiu a0, a0, -0x5918
.org 0x8015A6E8
	.ascii " Oi, "
	.byte 10
	.ascii ". "
	.byte 11, 15, 7
	.ascii " I love card games, just love 'em. "
	.byte 11, 15, 7
	.ascii " You can play me next time. "
	.byte 11, 15, 0, 0

.org 0x8015B0E4
	addiu a0, a0, -0x58C4
.org 0x8015A73C
	.ascii " Oh, "
	.byte 10
	.ascii ". "
	.byte 11, 15, 7
	.ascii " I love, love, love card games. "
	.byte 11, 15, 7
	.ascii " You can play me next time. "
	.byte 11, 15, 0, 0

.org 0x8015B104
	addiu a0, a0, -0x5874
.org 0x8015A78C
	.ascii "Mira"
	.byte 7, 16, 0, 105, 1, 17, 0, 19, 0, 105
	.ascii " Welcome home. "
	.byte 11, 15, 7
	.ascii " "
	.byte 10
	.ascii "."
	.byte 11, 15, 4, 17, 1
	.ascii " Are you ready to rest today? "
	.byte 11, 15, 0, 0

.org 0x8015B128
	addiu a0, a0, -0x5828
.org 0x8015A7D8
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
	.ascii " Speak to me over the counter. "
	.byte 11, 15, 0, 0

.org 0x8015B180
	addiu a0, a0, -0x57DC
.org 0x8015A824
	.byte 19, 0, 169
	.ascii " I won't go back, I promise. "
	.byte 11, 15, 7
	.ascii " 'cause my mom will get worried. "
	.byte 11, 15, 0, 0

.org 0x8015B194
	addiu a0, a0, -0x5794
.org 0x8015A86C
	.ascii " DANGER! "
	.byte 7
	.ascii "  Below, is the abandoned mine. "
	.byte 7
	.ascii "  Demons may appear. "
	.byte 0, 0

.org 0x8015B190
	addiu a0, a0, -0x5750
.org 0x8015A8B0
	.ascii "~The Warrior's Guide~"
	.byte 11, 15, 7
	.ascii " How to improve your skills,"
	.byte 7
	.ascii " it says. "
	.byte 11, 15, 0, 0

.org 0x8015B18C
	addiu a0, a0, -0x570C
.org 0x8015A8F4
	.ascii "~Notes on blacksmithing~"
	.byte 11, 15, 7
	.ascii " It describes how to blacksmith. "
	.byte 11, 15, 0, 0

.org 0x8015B158
	addiu a0, a0, -0x56CC
.org 0x8015A934
	.ascii "Witch "
	.byte 7
	.ascii " Shall I slot a"
	.byte 7
	.ascii " new spell for you? "
	.byte 11, 15, 12, 3, 0, 0

.close