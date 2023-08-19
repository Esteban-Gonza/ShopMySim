Interview task process:

To start this project, the first think I did was to design and plan the mechanics on the game. I made a list of mechanics that will be implemented: Player movement, player interaction with the environment, a shop system where the player could buy items and put it on. 
I download some basic assets package to have a preview on the game aesthetic, I didn’t spend much time on it because I wanted to focus on the mechanics first, so I create the player movement system, including basic animations using blend trees. This part was easy and simple, most of the time expended was to create a good shopping system.

For the shop I started creating the UI and a prefab of the icons on the shop, crating the possibility to add more items on the future, this was possible due to a data base of items that I implemented as Scriptable Objects. I wanted the information and player data to be saved locally, so I used a binary system to serialize the player information and store it on a file on the pc storage (the route should look like this: “C:\Users\user\AppData\LocalLow\DefaultCompany\ShopMySim_BGInterviewTask\GameData”). This allows me to have a way easier access to the player data, money, items purchased, and so on.

When I finished the shopping system, I focused on polish the interaction feature that I create along with the shop, but was too simple, it was just a debug feature. I decide that I wanted the player to interact with many objects in different ways, so I use Enum class to create categories on the objects, this allow me to create specific methods for every interactable object without using too much code and scripts. 
After all the technical features where almost done, I start to create the aesthetic of the game, along with objects and clothes animation, UI design, audio system, and the level design using tile maps to improve the design and development time.

There is plenty of things that can be added to the game, and some things can be improved. I would add an object pooling system instead of instantiating and destroy the items on the player. More items can be added, an inventory system as well, and a selling system. So far, I am happy with the result and would like to receive your feedback, I know I can improve a lot of pf things and I am open to suggestions.