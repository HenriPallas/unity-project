# Knifeback Chess
Use everything to win. That's all that matters.

A chess game where you can use everything and anything against your oppent and they in return do the same by changing the rules of chess on the fly.

To adapt, or not to adapt: that is the question:
Whether 'tis nobler in the mind to suffer
The slings and arrows of outrageous fortune,
Or to take arms against a sea of troubles,
And by opposing end them? To die: to lose;


## Game Rules
Like in normal chess, your goal is to capture the opponent’s king to win.
## Turn System
Players take turns one at a time.
Playing a card counts as your turn. You must wait until your next turn to move again.
## Card Mechanics
You earn a card every 5 turns.
Each player can hold up to 3 cards at a time.
White’s cards appear at the bottom of the screen.
Black’s cards appear at the top of the screen.
Some cards have special effects (like skipping the opponent’s turn or spawning pieces).
## Special Notes
If you play a card that skips the opponent’s turn, you get to take two turns in a row.
Cards can give you powerful advantages — use them wisely.


## Settup guide

Required software: Unity Hub, Unity 6000.0.44f1 , GitHub Destop.
The phone must have USB Debugging enabled.
Concerning Unity 6000.0.44f1 make sure it supports the mobile OS of your choice. If you don't have the tools or the correct version (6000.0.44f1) installed → go to Unity hub → Installs → Install Editor → Add modules (Android build support, OpenJDK, Android SDK and NDK tools and/or iOS build support).
![image](https://github.com/user-attachments/assets/b0534cb8-bdc7-447c-b376-66e5ea0ef88c)
![image](https://github.com/user-attachments/assets/0a07d84b-db52-4825-aae3-2bc23eb5c12d)
![image](https://github.com/user-attachments/assets/5b2aec7f-7453-43d0-a0a7-f63831c8dcd9)
![image](https://github.com/user-attachments/assets/9a01fd3b-fe71-4e3e-80f6-4256c1fae05b)
![image](https://github.com/user-attachments/assets/879d1b1a-1463-46e0-9dd1-e31a4fec30e7)


##Edit mode
1. Copy the following repository path URL: https://github.com/HenriPallas/unity-project
2. Open GitHub Desktop and navigate to File → Clone repository.
![image](https://github.com/user-attachments/assets/46c7f9ed-6d38-4a0c-86dc-60edccb0e7cb)

3. Choose the "URL" option, paste the copied URL and create a new folder for the project to reside in.
![image](https://github.com/user-attachments/assets/38aeb1f2-6a1e-4c5d-b474-9d4a0ee5fde8)
![image](https://github.com/user-attachments/assets/13bd4ba8-14a3-47a9-9cf7-de1e83ba8825)

5. Open Unity Hub and navigate to Add → Add project from disk. Type in the name of the project folder you chose and click on it.
![image](https://github.com/user-attachments/assets/883efad4-c974-4385-a42b-ac8bdf385acb)

7. Type in the name of the project folder you created, click on the game folder inside it. Click on it and "Add Project".
8. Open the project in Unity Hub and ta-da!

##Compile for mobile

1. File → build profiles → Switch platform to OS of your choice
![image](https://github.com/user-attachments/assets/a19f4da0-d106-4144-88d2-86568878ebb4)

3.  Enable developer mode. Next steps will vary greatly depending on the phone you have. If you run into issues use a search engine or ask an LLM how to reach developer mode with the phone model you have. For android usually go to settings → About phone → Software info → Click on Build number 15x in quick sessesion.
4.  Enable USB debugging.
5.  Connect your phone to your computer using a USB cable. Give any permission the phone asks.
6.  If all went right go to run device → refresh → dropdown your phone name or model number should show up
![image](https://github.com/user-attachments/assets/695f2d5f-3ff5-46c2-9f6f-12c89486a88a)

7. Go to Player settings → Player → Active input handling and choose → Input system package. Make sure you have this set to new not both.
![image](https://github.com/user-attachments/assets/2da38799-f6d8-48ba-9ed3-5ff3301a783e)
![image](https://github.com/user-attachments/assets/bad83b59-e289-4661-aa88-12d6ed5f3483)

8. Build and run. This PC → a folder on your pc of your choice eg. Downloads
Wait for it to compile and enjoy!



## Kodutööde sooritajad

- [x] Ül 1: Anna Berkman, Õnnela Link, Henri Rihard Pallas, Tormi Viirg.
- [x] Ül 2: Anna Berkman, Õnnela Link, Henri Rihard Pallas, Tormi Viirg.
- [x] Ül 3: Madis Valliste, Tormi Viirg.
- [x] Ül 4: Anna Berkman, Õnnela Link, Madis Valliste.
- [x] Ül 5: Henri Rihard Pallas, Madis Valliste, Tormi Viirg.
- [x] Ül 6: Henri Rihard Pallas, Madis Valliste, Tormi Viirg.
- [x] Ül 7: Anna Berkman, Õnnela Link, Madis Valliste.
- [x] Ül 8: Anna Berkman, Õnnela Link, Henri Rihard Pallas, Madis Valliste, Tormi Viirg.

## Homework documentation

- [x] Week 1:
What turned out to be easy and what turned out to be difficult:

Easy:
  Modifying the initial code (only minor changes to the code)
  Getting the debugger to function properly
  Connecting with Android
  Sharing ideas
  Internal group communication
  Finding a common time

Difficult:
  Opening the Unity project from GitHub Desktop
  
Project Requirements:
  Create the project publicly on GitHub
  Create a setup guide
  Modify 1% of the initial template code
  Ensure the application works on both a physical mobile device and an emulator
  Ensure the debugger works on a physical device as well

- [x] Week 2:
C# overview
 (https://docs.google.com/document/d/13vMeCIawrKJIbYmVk_AHcN1xxpTRCkaAnYMo8r03aaQ/edit?tab=t.0)

- [x] Week 3:
Process:
  In the beginning, we found placeholder assets and started working on the basic chess logic. We built a system that automatically generates the board and places the pieces. Then we added the button logic and a basic card system. In the   end, we created a quick main menu mockup and made some code fixes.

Easy:
  One of the easiest parts was using the assets and setting up how they should work in Unity.

Difficult:
  One of the most difficult parts was building the chess logic in a way that was forward thinking and integratable with our card system while being easy enought to implement on a very tight scheddule and figuring out how to assign     
  multible people to it in a way they could work on it simultaniously. 
  Designing a UI that best communicates the idea of our game while accounting for the limitations of the mobile format.
  Actually agreeing on a idea that's doable in the timeframe provided with the possibility to reuse code from the individual weekly assignments in the final game. Quite the task as peoples expectations for games are very high and the      games themselves very difficult to make because of their highly interactable and non-standardised way of doing things. 

- [x] Week 4:
  - Protsess:
    Implementing and using microphones, cameras, and sensors like the accelerometer on our phones using Unity turned out to be an interesting experience.
  - Easy:
    Overall, one of the easier parts was making use of the modules already built into Unity using the built-in APIs such as the Microphone, WebCamTexture, and Input classes, which allowed us to set up and use device inputs quite             quickly. For example, recording audio from the microphone or displaying the camera feed in real-time on a separate screen worked quite smoothly.
  - Difficult:
    However, processing the data and handling permissions proved to be more challenging. For instance, sometimes the accelerometer and gyroscope did not behave as expected on different devices. In the end, though, we managed to       
    integrate them into the game logic as well.

- [x] Week 5:
  - Protsess: To create a new AR page, We started by choosing a suitable template in Unity using AR Foundation. The template helped to speed up the initial setup, especially with camera configuration and basic AR plane detection. We         added a 3D model and added interactivity by adding the additional challange of finding where the object spawned in the enviornment.

  -Easy:
    One of the easiest parts of the process was placing the 3D content in the scene and seeing it rendered through the AR camera. Unity’s interface and prefab system made it quick to test different objects.
  -Difficult
    However, one of the more difficult challenges was ensuring that AR features worked consistently across different mobile devices. Debugging AR plane detection and touch controls was also tricky, especially when the environment had         poor lighting or low-texture surfaces.

- [x] Week 6:
  - Protsess:
    In this project, we created a personal settings page where user preferences are stored locally on the mobile device. For local storage, we used Unity's PlayerPrefs for simple settings like toggles, and for more complex         data,     we wrote the data directly to the device's persistent data path using System.IO to implement a sort of save system for rollbacks as our second card. These files are saved in the application's persistent data folder, so they               remain accessible even after the app is closed and reopened.
  - Easy:
    Saving the players name and creating a scoreboard.
  -Difficult
    Saving data was supprisingly hard using built-in C# file handling and Unity paths. The challenge was ensuring proper permissions, and especially coneverting the board state into a usable and efficient file format, while dealing with     buggs on android. Handling different file paths on iOS and Android also required some platform-specific code which meant we were unable to properly test our card and thus couldn't use it in our final version of the game. We also ran     into difficulties allowing the user upload a photo and autocropping it so it would fit the 1:1 aspect ratio.

- [x] Week 7:
  - Protsess: 
  Integrate our mobile application with an external server, we implemented a lightweight solution using RESTful HTTP communication. The app connects to a public API (like https://reqres.in) to fetch and send information in JSON format.
  First, we selected the data source—an API endpoint that provides user data, including names, and the locations of our pices on the board. The HTTP protocol with REST was chosen because it's simple and widely supported in Unity using     UnityWebRequest. Once connected, we handled data fetching by making GET requests. The received JSON data was parsed and then used to dynamically display user information and images in the app. For sending information, we used POST       requests to simulate data submission to the server—such as user feedback or profile settings. The data was serialized into JSON and sent along with the request body.

  -Easy:
  Leaderboard
  -Difficult:
  One major challenge was managing asynchronous requests and ensuring the UI updates correctly after the data is received. Another was handling server errors or offline situations gracefully and sending the coordinates.

- [x] Week 8:
  - Protsess:
    Implementing a turn system in our splitscreen local game where playing a card counts as your turn. You must wait until your next turn to move again. You earn a card every 5 turns and each player can hold up to 3 cards at a time. White’s cards appear at the bottom of the screen, black’s cards appear at the top of the screen. Some cards have special effects (like skipping the opponent’s turn or spawning pieces). Implementing and designing the finnal UI and menu system for navvigation for it.
  - Easy:
    UI design and implementation with the corrosponding menus.
  - Hard:
    Making new rule cards.

## Authors
 Anna Berkman, Õnnela Link, Henri Rihard Pallas, Madis Valliste, Tormi Viirg
