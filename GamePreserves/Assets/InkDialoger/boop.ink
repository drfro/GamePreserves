-> test

=== test
You stare at the monster in front you
 I don't know what you want from me here #bill
 dude, shut up #bob
- (rechoice)
* [I don't...] ...know what the heck is going on here. -> exclamation
+ [I... ok?] -> narrator
- (narrator)
 we are speaking from an ink derived text file. #bob
 oh, yeah.  The file is exported to JSON from the ink engine and re-imported as a text asset. #bill
 yes, and then this dialogue engine also is checking for a name at the beginning. #bob
 yeah, if you don't specifiy someone, that's assumed to be the narrator #bill
YES, GOOOD. #god
  JESUS #bill #bob
- (reask)
* [why do I care?] -> why_care
* [what does this do for me?] -> what_do
+ [I think I get it.] -> ending

= why_care
 That's a great question #bill
 Yeah.  Well, I mean, have you ever had to manage dialogue? #bob
 actually, yeah, it sucks. #bill
-> reask

= what_do
 how do I use this? #bill
 you will need to apply the component correctly, to any arbitrary object that will control this particular set of dialogue. #bob
-  Then, you will need to add one or more speakers to the speakers list. #bob
* ok...
-  Make sure that you name them correctly! Otherwise, lines won't line up.  #bob
You should also add some sort of visual effectors as well, otherwise only the narrator will be speaking.
-> reask

= ending
 I think you get it now. #bob
 toodles! #bill
-> DONE

=== exclamation
 I am not sure either. #bob
 This is a tech demo, dumbass #bill
+ oh? -> test.rechoice


-> DONE


