# Thank you for your time reviewing my test assignment!
Unity version used: 2022.3.57 LTS

Here will be provided a small description of projects functionality. 
I had some troubles with UI of the wheel, so now it won't scale as intended. 
I could easily fix it by removing functionality to generate different amount of rewards, 
or spend a little more time to figure it out, but I already missed my deadline for a good while, 
so I'll leave it like that for now.

# Bootstrap
I decided to create an EntryPoint for each scene. It will help me to control the order of 
initialization and make it easier to setup everything. When scene is loaded, 
EntryPoint will init everything and pass every needed refferences. 

# Fortune Wheel functionality:
First of all, the main mechanic. 

<br>Main scripts here are <b>FortuneWheelView, FortuneWheelController and FortuneWheelModule.</b>
<br>Changing FortuneWheelConfig.asset you can configure spinner variables such as:
<li><b>LowestValue</b> - Lowest reward value
<li><b>BiggestValue</b> - Biggest reward value
<li><b>StepValue</b> - Minimal step between rewards
<li><b>RewardsCount</b> - Amount of rewards that are generated. (Big values will cause problems with UI)
<li><b>SpinsAmount</b> - How many times wheel will spin before getting to end position
<li><b>AnimationTime</b> - How long wheel would spin before stop
<li><b>SpinningCurve</b> - How rotation speed will change in time

<h3><b>Logic:</b></h2>

1. When FortuneWheelView is inited, it will reffer to FortuneWheelController to generate possible rewards. Controller will transfer this request to Module and return generated rewards. View will instantiate UI elements for rewards.
2. When user clicks "SPIN" button, FortuneWheelView script will reffer to FortuneWheelController to get index of reward that was won by player. Then, reward will be selected by this index and Spinning animation will begin.
3. After wheel fully stop, reward will be added to player's account and saved to Prefs (can be changed to be saved in json or sent to the server).
4. RewardPopup will be shown, telling player which reward he get.
5. After player clicks "Claim" button, FortuneWheelView will reset and we will go to p.1.

<p> Model can be easily replaced with another logic without any problems, 
as long as the new class will implement IFortuneWheelModule interface.

# Currency Panel
Player CurrencyPanel subscribes to onValueChanged event in ReactiveProperty of PlayerData.CoinsAmount
and updates UI every time the value was changed.

# UI
UI is divided into two different canvases - one for UI and one for gameplay wheel. 
Just simple canvas grouping