->shopkeep

==shopkeep
=acquaintance
{Oh, you're having trouble aren't you? I've got guns that can help.|Beat slugs to earn points to buy my guns. Then you can beat more slugs.|Ohh, you were so close, weren't you? One more purchase should do it.|You're not getting much of anywhere, are you?|One more try'll do it, yeah? I'm sure of it.|->truthtime}
->shoptalk
=truthtime
{No matter how many times you try, you just aren't getting ahead.|Is there something you forgot keeping you back?|I guess it's too much to expect a Roomba to remember everything.|How do you repent for something you can't remember?|I sent an apology card to somebody I didn't know once.|They forgave me.|Robots don't make their own choices, but they can still hurt people.|Where does the weight of a sin with no will go?|You've beaten so many slugs, but you still come back here.|Maybe next time, you'll beat them forever.|->cynicism}
->shoptalk
=cynicism
{improvement: ->turnaround|{~I guess not.|I guess not.|Oh? Still pressing forward, even though you know you can't win?->improvement}->shoptalk}
=turnaround
{There's no reason to keep going, but you still try to make things better.|That's admirable! I've turned around on you, roombot.|Maybe there's something that can change after all.|->optimism}
->shoptalk
=optimism
{You know, there may be a way to end this.|As long as there's enemies, you have to fight them.|There's only one place where the slugs never go.|You may be a robot, but even you're making choices here.|You see what I mean, yeah? Go ahead and try to Escape!|What, do you think you'll miss me?|Don't worry, you can always start over and meet me again.|Maybe you're having too much fun. Can't blame you!|Get that high score!}
->shoptalk
#end
->DONE

==shoptalk
//+[Big]
//{shopkeep.truthtime:Firing blindly will always hit something.|A great choice! Can't miss if they fill the screen, yeah?}->shoptalk
+[Pistol]
Really?! Why!? ->shoptalk
//+[Swaffer]
//{shopkeep.truthtime:Never have to be vulnerable with your clean shield.|A good choice! They can't get close if you keep it clean.}->shoptalk
+[Shotgun]
{shopkeep.truthtime:As long as they're nearby, they're done for. But that goes for you too, doesn't it?|A good choice! Whole hordes will be afraid of you now.}->shoptalk
+[Sniper]
{shopkeep.truthtime:If they never get close, they can't hurt you. But you can only see so many problems.|A good choice! You'll take enemies out before they even get close!}->shoptalk
+[Globe]
{shopkeep.truthtime:Take away control, and let the world choose for you.|A good choice! Those slugs won't know what hit 'em!}
->shoptalk
+[Finish]
{shopkeep.optimism:Just keeping me company? I can't say I mind it!|{shopkeep.truthtime:Do you think that will help?|Some good choices! Try yourself out and beat those slugs!}}
#end
->DONE
*{shopkeep.optimism}[Quit]
The only way to change your luck is to make a choice you haven't before! Go outside and have some real fun.
#end
->DONE


==improvement
->shoptalk

#end
->DONE