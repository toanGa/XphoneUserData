- Create text data
   + In OS project: "LOAD_TEXT_FROM_NAND = 0" (confirm text init success). Then use phone for all languages
   + After confirm init text, set ""LOAD_TEXT_FROM_NAND = 1" and build project. This output will be used to update firmware
   + In C# project: Click "Brows" the select the foder text (libs/text) in OS project
                    Click "Genenrate" to generate file "sys_text.bin"
   + In SFH project:
                    Copy "sys_text.bin" has generated to this foder
					To Write "sys_text.bin" to Phone, you select option "User Data" and "System Text" then use SFH tool as normaly
   + Mỗi lần có sự thay đổi text thì đều lặp lại quá trình này