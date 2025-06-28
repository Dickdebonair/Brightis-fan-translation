.psx
.open "OVR\046.bin", 0x80158138

.org 0x8015A5BC
	addiu a0, a0, -0x7EC8
.org 0x80158138
	.byte 16, 1, 110, 2, 16, 1, 111, 2, 16, 1, 112, 2, 16, 0, 178, 1, 19, 0, 178
	.ascii " Hey hey, nice sword. "
	.byte 11, 15, 7
	.ascii " With those glowbrand weapons, "
	.byte 11, 15, 7
	.ascii " You get some awesome stuff. "
	.byte 11, 15, 4
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "Oh, you don't have any ore? "
	.byte 11, 15, 7
	.ascii " Try checking in the mines. "
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
	.ascii " Each has its own attributes, "
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
	.ascii " Read the poster there. "
	.byte 11, 15, 4
	.ascii " If you think you can, try it"
	.byte 11, 15, 7
	.ascii " But, if you think you can't"
	.byte 11, 15, 7
	.ascii " You should leave it to me."
	.byte 11, 15, 14, 3, 17, 3, 0, 0

.org 0x8015A5B8
	addiu a0, a0, -0x7BE4
.org 0x8015841C
	.byte 17, 0, 16, 1, 108, 2, 16, 1, 107, 2, 16, 0, 179, 1, 19, 0, 179
	.ascii " Oooh, you have ore. "
	.byte 11, 15, 7
	.ascii " Yes, let me tell you about it. "
	.byte 11, 15, 7
	.ascii " 3 types: red, blue, & yellow. "
	.byte 11, 15, 4
	.ascii " Each has its own attributes, "
	.byte 11, 15, 7
	.ascii " Red is fire, blue is water. And "
	.byte 11, 15, 7
	.ascii " yellow is lightning. "
	.byte 11, 15, 4
	.ascii " With that ore, "
	.byte 11, 15, 7
	.ascii " I can attribute the weapon, "
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
	.ascii " I can build you a new one. Well? "
	.byte 11, 15, 4
	.ascii " Or you can try yourself? "
	.byte 11, 15, 7
	.ascii " Read the poster there. "
	.byte 11, 15, 4
	.ascii " If you think you can, try it "
	.byte 11, 15, 7
	.ascii " But, if you think you can't, "
	.byte 11, 15, 7
	.ascii " You should leave it to me. "
	.byte 11, 15, 17, 3, 0, 0

.org 0x8015A5B4
	addiu a0, a0, -0x7968
.org 0x80158698
	.byte 19, 0, 178, 19, 0, 179, 19, 1, 104
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
	.ascii " I can build you a new one. Well? "
	.byte 11, 15, 4
	.ascii "You can try to build it yourself, "
	.byte 11, 15, 7
	.ascii " Read the poster there. "
	.byte 11, 15, 4
	.ascii " If you think you can, try it. "
	.byte 11, 15, 7
	.ascii " But, if you think you can't, "
	.byte 11, 15, 7
	.ascii " you should leave it to me. "
	.byte 11, 15, 0, 0

.org 0x8015A5CC
	addiu a0, a0, -0x7744
.org 0x801588BC
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
	.ascii " You cast by opening the menu."
	.byte 11, 15, 7
	.ascii " Set the spell in the menu & "
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

.org 0x8015A5E4
	addiu a0, a0, -0x7540
.org 0x80158AC0
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
	.ascii " you get something amazing. "
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
	.ascii " You've gotta choose one to use. "
	.byte 11, 15, 7
	.ascii " But without a sword... "
	.byte 11, 15, 0, 0

.org 0x8015A574
	addiu a0, a0, -0x7394
.org 0x80158C6C
	.ascii "Pollux "
	.byte 7, 16, 0, 102, 1, 17, 0, 19, 1, 98, 19, 0, 102
	.ascii " Hey, when attacking an enemy,"
	.byte 11, 15, 7
	.ascii " a number appears, right? "
	.byte 11, 15, 4
	.ascii " That's the SP you've earned. "
	.byte 11, 15, 7
	.ascii " Unlike EXP, it doesn't "
	.byte 11, 15, 7
	.ascii " matter how strong the enemy is. "
	.byte 11, 15, 4
	.ascii " Only on how much you attacked. "
	.byte 11, 15, 7
	.ascii " AKA, the better your attack, "
	.byte 11, 15, 7
	.ascii " the more SP gained. "
	.byte 11, 15, 4
	.ascii " So, if you want to earn more SP, "
	.byte 11, 15, 7
	.ascii " equip weaker weapons, and"
	.byte 11, 15, 7
	.ascii " you'll get a lot. "
	.byte 11, 15, 14, 2, 17, 1
	.ascii " Oh, what's that? "
	.byte 11, 30, 7
	.ascii " You want to learn some tricks? "
	.byte 11, 15, 17, 2, 12, 4, 0, 0

.org 0x8015A598
	addiu a0, a0, -0x71EC
.org 0x80158E14
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
	.ascii " Can you bring me a nightshade? "
	.byte 11, 15, 4
	.ascii " With it, we can make many "
	.byte 11, 15, 7
	.ascii " more wonderful medicines. "
	.byte 11, 15, 7
	.ascii " I know it's a lot to ask, but- "
	.byte 11, 15, 0, 0

.org 0x8015A594
	addiu a0, a0, -0x7140
.org 0x80158EC0
	.ascii "Hamal"
	.byte 7, 16, 0, 109, 1, 17, 0, 19, 0, 109, 19, 0, 170
	.ascii " Hello again, "
	.byte 10
	.ascii ". "
	.byte 11, 15, 7
	.ascii " Oh? ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 20
	.ascii "Is that nightshade... "
	.byte 11, 45, 4
	.ascii " That plant is rare & precious "
	.byte 11, 15, 7
	.ascii " it only grows with magic."
	.byte 11, 15, 7
	.ascii " It's a wonderful medicinal herb. "
	.byte 11, 15, 4
	.ascii " With it, we can make many"
	.byte 11, 15, 7
	.ascii " more powerful medicines. "
	.byte 11, 15, 7
	.ascii " Please, "
	.byte 11, 15
	.ascii "let me have it! "
	.byte 11, 15, 14, 3, 17, 1
	.ascii " Oh, pardon me. "
	.byte 11, 15, 7
	.ascii " What can I do for you? "
	.byte 11, 15, 17, 2, 12, 2, 17, 3, 0, 0

.org 0x8015A578
	addiu a0, a0, -0x6FFC
.org 0x80159004
	.ascii "Regulus "
	.byte 7, 16, 0, 103, 1, 17, 0, 19, 1, 99, 19, 0, 103
	.ascii " I heard about you, well done. "
	.byte 11, 15, 7
	.ascii " I wanted to tell you one thing. "
	.byte 11, 15, 4
	.ascii " If you meet a man called"
	.byte 7
	.ascii " Rasalhague, "
	.byte 11, 15, 7
	.ascii " Absolutely run away, okay? "
	.byte 11, 15, 4
	.ascii " You'll never win right now. "
	.byte 11, 15, 7
	.ascii " You need more experience. "
	.byte 11, 15, 7
	.ascii " I'll teach you all I know. "
	.byte 11, 15, 14, 2, 17, 1
	.ascii " Here to learn swordsmanship? "
	.byte 11, 15, 7
	.ascii " Which technique? "
	.byte 11, 15, 17, 2, 12, 6, 0, 0

.org 0x8015A5A8
	addiu a0, a0, -0x6EBC
.org 0x80159144
	.ascii "Alfeca"
	.byte 7, 16, 0, 106, 1, 17, 0, 19, 0, 106
	.ascii " Hmm."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii " it's you... "
	.byte 11, 30, 7
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "I have something to say. "
	.byte 11, 15, 4
	.ascii " Haven't you had enough?"
	.byte 11, 15, 7
	.ascii " Don't provoke the demons. "
	.byte 11, 15, 4
	.ascii " What if the demons get angry "
	.byte 11, 15, 7
	.ascii " & attack this town? "
	.byte 11, 45, 7
	.ascii " Don't cause trouble for us! "
	.byte 11, 15, 4, 17, 1
	.ascii " Are you going to protect us"
	.byte 11, 15, 7
	.ascii " if something happens? "
	.byte 11, 15, 0, 0

.org 0x8015A5C4
	addiu a0, a0, -0x6D78
.org 0x80159288
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "So the demons collecting them."
	.byte 11, 15, 7
	.ascii " If collected: all 4, with the  "
	.byte 11, 15, 7
	.ascii " dark element, if made complete."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 11, 15, 4
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "Hey, don't even think about it! "
	.byte 11, 15, 7
	.ascii " you can't give it to the demons. "
	.byte 11, 15, 7
	.ascii " Okay?? Alright! "
	.byte 11, 15, 4
	.ascii " Besides, if you have it,"
	.byte 11, 15, 7
	.ascii " YOU can use powerful magic... "
	.byte 11, 15, 12, 3, 0, 0

.org 0x8015A5C0
	addiu a0, a0, -0x6C58
.org 0x801593A8
	.ascii "Witch "
	.byte 7, 19, 0, 108
	.ascii " "
	.byte 10
	.ascii ", glad you're here. "
	.byte 11, 15, 7
	.ascii " About the global element-"
	.byte 11, 15, 7
	.ascii " I did a little research. "
	.byte 11, 15, 4
	.ascii " In the lore, the global element "
	.byte 11, 15, 7
	.ascii " is a piece of the dark element. "
	.byte 11, 15, 4
	.ascii " The most massive and powerful, "
	.byte 11, 15, 7
	.ascii " the dark element."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii ". whoever "
	.byte 11, 15, 7
	.ascii " holds it can rule this world. "
	.byte 11, 15, 0, 0

.org 0x8015A5D0
	addiu a0, a0, -0x6B48
.org 0x801594B8
	.ascii "Capella "
	.byte 7, 16, 0, 110, 1, 17, 0, 19, 0, 110
	.ascii " Hey, "
	.byte 10
	.ascii ". "
	.byte 11, 45, 7
	.ascii " Aren't the demons more active? "
	.byte 11, 30, 7
	.ascii " What's up with that? "
	.byte 11, 15, 4
	.ascii " Especially in the Feramen Mts., "
	.byte 11, 15, 7
	.ascii " They showed up at the Falls, "
	.byte 11, 15, 7
	.ascii " just west of the foothills. "
	.byte 11, 15, 4, 17, 1
	.ascii "The Falls are my hunting ground, "
	.byte 11, 15, 7
	.ascii " so it's trouble. What to do."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 11, 15, 0, 0

.org 0x8015A580
	addiu a0, a0, -0x6A38
.org 0x801595C8
	.ascii "Lesart "
	.byte 7, 16, 0, 104, 1, 17, 0, 19, 0, 104
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "I dream a lot these days. "
	.byte 11, 30, 7
	.ascii " about the ruins in the Mts."
	.byte 11, 15, 7
	.ascii " I dreamt darkness covered us. "
	.byte 11, 15, 4
	.ascii " Terrifying even to remember."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 11, 15, 7
	.ascii " Something bad is coming. "
	.byte 11, 15, 7
	.ascii " I hope it's not a bugbear... "
	.byte 11, 15, 4, 17, 1
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "What am I going to do... "
	.byte 11, 30, 0, 0

.org 0x8015A5AC
	addiu a0, a0, -0x6930
.org 0x801596D0
	.ascii "Sadal"
	.byte 7, 16, 0, 107, 1, 17, 0, 19, 0, 107
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "You look good."
	.byte 7
	.ascii " Here to buy something? "
	.byte 11, 15, 4
	.ascii " Now that the boats are cut off,"
	.byte 11, 15, 7
	.ascii " Not a lot of good stuff, but "
	.byte 11, 15, 7
	.ascii " I oversee it, so they're quality. "
	.byte 11, 15, 4, 17, 1
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "What do you need? "
	.byte 11, 15, 7
	.ascii " I wish I had something new in. "
	.byte 11, 15, 12, 1, 0, 0

.org 0x8015A57C
	addiu a0, a0, -0x683C
.org 0x801597C4
	.ascii "Capella "
	.byte 7, 16, 0, 110, 1, 17, 0, 19, 0, 110
	.ascii " Hey, "
	.byte 10
	.ascii ". "
	.byte 11, 45, 7
	.ascii " Aren't the demons more active? "
	.byte 11, 30, 7
	.ascii " What's wrong with them? "
	.byte 11, 15, 4
	.ascii " Even in the Feramen Mountains, "
	.byte 11, 15, 7
	.ascii " They're at the Falls too, "
	.byte 11, 15, 7
	.ascii " just west of the foothills."
	.byte 11, 15, 4, 17, 1
	.ascii " Maybe we should "
	.byte 7
	.ascii " go & have a look."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 11, 15, 0, 0

.org 0x8015A56C
	addiu a0, a0, -0x674C
.org 0x801598B4
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

.org 0x8015A568
	addiu a0, a0, -0x667C
.org 0x80159984
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
	.ascii " In the dorms where Regulus lives, "
	.byte 11, 15, 7
	.ascii " Read the Warrior's Guide. "
	.byte 11, 15, 7
	.ascii " It will help you. "
	.byte 11, 15, 0, 0

.org 0x8015A5A4
	addiu a0, a0, -0x65B8
.org 0x80159A48
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
	.ascii " There's a poster on how to play. "
	.byte 11, 15, 7
	.ascii " Read it in your spare time. "
	.byte 11, 15, 0, 0

.org 0x8015A588
	addiu a0, a0, -0x64FC
.org 0x80159B04
	.ascii " I see... Well "
	.byte 11, 15, 5
	.ascii " Don't be discouraged. He... "
	.byte 11, 30, 7
	.ascii " Alfeca... also... "
	.byte 11, 15, 7
	.ascii " Regret won't change the past... "
	.byte 11, 15, 5
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "Sorry, I'm complaining... "
	.byte 11, 15, 0, 0

.org 0x8015A570
	addiu a0, a0, -0x6468
.org 0x80159B98
	.ascii "Archena"
	.byte 7, 16, 0, 161, 2, 16, 0, 101, 1, 17, 0, 19, 1, 97, 19, 0, 101
	.ascii " ."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "Andrew."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 11, 15, 14, 3, 17, 1, 19, 0, 161
	.ascii " Hmm, "
	.byte 11, 15
	.ascii " what is it? "
	.byte 11, 15, 5, 17, 2
	.ascii " Here to learn sword dance? "
	.byte 11, 15, 7
	.ascii " Which technique? "
	.byte 11, 15, 12, 5, 17, 3, 0, 0

.org 0x8015A564
	addiu a0, a0, -0x63D8
.org 0x80159C28
	.ascii " If you want to learn to fight, "
	.byte 11, 15, 7
	.ascii "The Warrior's Guide inside, "
	.byte 11, 15, 7
	.ascii " You might want to read it. "
	.byte 11, 15, 5
	.ascii " It might help you. "
	.byte 11, 15, 0, 0

.org 0x8015A5D8
	addiu a0, a0, -0x635C
.org 0x80159CA4
	.ascii "~How to Play Cards~ "
	.byte 11, 15, 7
	.ascii "A popular game from the empire, "
	.byte 11, 15, 7
	.ascii " Here describes how to play. "
	.byte 11, 15, 0, 0

.org 0x8015A59C
	addiu a0, a0, -0x6300
.org 0x80159D00
	.ascii " Oh! Thank you!!! "
	.byte 11, 15, 7
	.ascii " We can make better medicine! "
	.byte 11, 30, 7
	.ascii " Please come back in a while. "
	.byte 11, 15, 0, 0

.org 0x8015A5A0
	addiu a0, a0, -0x62A8
.org 0x80159D58
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

.org 0x8015A5B0
	addiu a0, a0, -0x6254
.org 0x80159DAC
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

.org 0x8015A5E8
	addiu a0, a0, -0x6204
.org 0x80159DFC
	.ascii " DANGER! "
	.byte 7
	.ascii "  Below, is the abandoned mine. "
	.byte 7
	.ascii "  demons may appear. "
	.byte 0, 0

.org 0x8015A590
	addiu a0, a0, -0x61C0
.org 0x80159E40
	.ascii "Mira "
	.byte 7
	.ascii "　Are you ready to go to bed? "
	.byte 11, 15, 0, 0

.org 0x8015A5E0
	addiu a0, a0, -0x619C
.org 0x80159E64
	.ascii "~The Warrior's Guide~"
	.byte 11, 15, 7
	.ascii " It says:"
	.byte 7
	.ascii " How to improve your skills. "
	.byte 11, 15, 0, 0

.org 0x8015A584
	addiu a0, a0, -0x6158
.org 0x80159EA8
	.ascii "Mira "
	.byte 7, 19, 0, 105
	.ascii " Ah, "
	.byte 10
	.ascii ", "
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 30, 7
	.ascii " How are you? Found others? "
	.byte 11, 15, 0, 0

.org 0x8015A5DC
	addiu a0, a0, -0x6118
.org 0x80159EE8
	.ascii "~Notes on blacksmithing~"
	.byte 11, 15, 7
	.ascii " It describes how to blacksmith. "
	.byte 11, 15, 0, 0

.org 0x8015A5C8
	addiu a0, a0, -0x60D8
.org 0x80159F28
	.ascii "Witch "
	.byte 7
	.ascii " Shall I slot some"
	.byte 7
	.ascii " new magic for you? "
	.byte 11, 15, 12, 3, 0, 0

.org 0x8015A58C
	addiu a0, a0, -0x60A4
.org 0x80159F5C
	.ascii " Um. "
	.byte 10
	.ascii "."
	.byte 11, 10
	.ascii "."
	.byte 11, 10, 11, 30, 7
	.ascii " Please have a good rest. "
	.byte 11, 15, 0, 0

.close