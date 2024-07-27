### Intro text Crawl: 
- Text: 
```
世の国々が大陸内の番権笠争った 			The nations of the world fought for dominion over the continent
永き戦乱に疲抽し。									Tired of the long warfare.
土族ら世代全貸邊 きっか(ま皮 				The generations of the earthly tribes lent their skins to the war.
休戦へと流れ 											Toward a truce
天った冨と国力を回復すべく興和を使い 	to restore the wealth and power of the country.
```
 - location in RAM: TBD
 - location in ROM/file: TBD
 - how to trigger in game: Opening text [start of game]

### Talking with the Wizard, Wado:
- Text: 
```
ワドー										WADO.
気がついたようじゃな。			I see you've noticed [me].
どうじゃ・・・立てるか?			How about [it]... can you stand?
```
 - location in RAM: `0x8015c9c8`
 - location in ROM/file: TBD
 - how to trigger in game: intro to game with Wado.

### Other Misc. text 
- Text: `ＳＫＩＬＬ` [Shift-JIS Encoded]
	- location in RAM: TBD
 	- location in ROM/File: `0x000B2D30` [Full Game BIN]
  - how to trigger in game: Pause Menu, first screen

- Text: `キーコンフィグ` key config
	- location in RAM: TBD
  - location in ROM/FIle: `0x000CEA42`[Full Game BIN]
  - how to trigger in game: pause menu, far right option

- Text: `【はい】／　【いいえ】` Yes / No
	- location in RAM: TBD
  - location in ROM/FIle: `0x000B29E2`[Full Game BIN]
  - how to trigger in game: used in multiple places, Load game is first

- Text: `休憩しますか？` Do you want to take a break?
	- location in RAM: TBD
  - location in ROM/FIle: `0x000B29FC`[Full Game BIN]
  - how to trigger in game: Use a fountain [Dungeon]

### Player Name
- Stored in RAM [breakpoint]: `8001e4e8`