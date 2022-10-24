# Class Notes

You can download this entire project: [Link](https://github.com/theshaneobrien/DGH1Resources/archive/refs/heads/main.zip)

You can find the EnemySpawner script we wrote here: [Link](https://github.com/theshaneobrien/UnityScriptingResources/blob/main/Class1-Functions-Lists-Loops/UnityProject/Assets/Scripts/EnemySpawner.cs)

You can find a version with detailed comments here: [Link](https://gist.github.com/theshaneobrien/3a240d854d773e9975c01c57ee138f2c)

I've kept the comments separate as, while useful, too many comments can make code difficult to read, so make sure to reference the commented one while reading the "clean" one.

## Here's what we did in the Unity Editor
1. We added a "ScriptMule" GameObject to the scene, this is a gameObject that we put as much GameLogic scripts on as we can, mainly to prevent us going searching in the Unity scene trying to find where our scripts actually are. The idea is to minimize the amount of rogue scripts in our scene, thus making it much easier to find everything.
2. We attached our EnemySpawner script to this GameObject:
![Image of the above](https://github.com/theshaneobrien/DGH1Resources/blob/e888eeebcf865c943346ce7e86e89868f50694b3/DG1-Class1-Functions-Lists-Loops/Screenshots/1..jpg "1")

3. We added all of our spawn points to the spawnPoints List, we use a handy feature to do this, you select the gameObject you want to add multiple items to, click the lock button up the top right (1), select all the gameObjects you wish to add (2), and drag them to the List you wish to populate (3)
![Image of the above](https://github.com/theshaneobrien/DGH1Resources/blob/e888eeebcf865c943346ce7e86e89868f50694b3/DG1-Class1-Functions-Lists-Loops/Screenshots/2.jpg "1")

4. We made a prefab by dragging a gameObject from the hierarchey to the Prefabs folder, you can read more about prefabs here: https://docs.unity3d.com/Manual/CreatingPrefabs.html
![Image of the above](https://github.com/theshaneobrien/DGH1Resources/blob/e888eeebcf865c943346ce7e86e89868f50694b3/DG1-Class1-Functions-Lists-Loops/Screenshots/4.jpg "1")
![Image of the above](https://github.com/theshaneobrien/DGH1Resources/blob/e888eeebcf865c943346ce7e86e89868f50694b3/DG1-Class1-Functions-Lists-Loops/Screenshots/5.jpg "1")


## Bonus
Folder Structure is very important, it might seem silly but keeping your project neat and tidy speeds up development and prevents major headaches, so find a good folder structure that works for you and develop the habit of keeping it well organized.
![Image of the above](https://github.com/theshaneobrien/DGH1Resources/blob/e888eeebcf865c943346ce7e86e89868f50694b3/DG1-Class1-Functions-Lists-Loops/Screenshots/3.jpg "1")

The console is where Unity will tell you about any errors, but you can also use it to print Debug Logs to help you understand what is happening in your game. You can read more about Debug Logs here: https://docs.unity3d.com/ScriptReference/Debug.Log.html
![Image of the above](https://github.com/theshaneobrien/DGH1Resources/blob/e888eeebcf865c943346ce7e86e89868f50694b3/DG1-Class1-Functions-Lists-Loops/Screenshots/6.jpg "1")
