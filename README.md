# Raise a BonBon!
The project is for a Unity AR Mobile Application that acts like a tamagotchi where users will take care of a creature with the various items and buttons. The app syncs with Firebase for user authentication as well as to save past play data for future use.

# Design Process
As a person that likes to take care of cute little creatures, I want to play this game so that I can take care of a creature on my phone whever I am free.
As someone that wants unique experiences, I want to play with AR so that I can get a new experience compared to other similar tamagotchi-like games.


# Features
In this section, you should go over the different parts of your project, and describe each in a sentence or so.

## Existing Features
- Feature 1 - Creating new user data  (website) - Users are able to sign up through the website to start playing the game.
- Feature 2 - Retrieving current user data - Website and Unity Application are able to retrieve user data from Firebase and will use the data for it's own functions
- Feature 3 - Updating user data (Unity) - Unity application is able to update user's save data so that future sessions will take values from the previous session.
- Feature 4 - Spawning Bon when flat ground is detected - Plane detection is used so that when a horizontal plane is detected, the bon will spawn and the game will start
- Feature 5 - Ar Image detection and item spawning - corresponding items will spawn when the correct images are placed in front of the camera. These items have different functionality in the game.

In addition, you may also use this section to discuss plans for additional features to be implemented in the future:

## Features Left to Implement
Multiple possible Bons - It would've been nice to have more choices in which creature to use and to implement creature preferences but there wasn't enough time to implement it
Async changes in data based off time not playing the game - To make it so that users will be more motivated to play the game frequently as the game currently maintains the creatures stats from log out.
Creating new Bon when playing with current one for long enough - lends more variety to the application which will keep users interested

# Testing
1. Go to website to create a new account
2. Enter email and password for new account
3. Go to the "logInPage" page in Unity Application
4. Log in to the application using valid email and password
5. Enter name for bon
6. Choose bon type
7. Move camera to find a valid flat ground to spawn bon
8. Wait for bon's stats to decrease enough that user will want to increase their stats back to full
9. Use image detection to spawn food for bon
10. Feed bon
11. Use image detection to spawn shower for bon
12. Clean bon
13. Click UI button for sleeping to make the bon rest
14. Click UI button again when bon is fully rested to stop sleep
15. Click UI button for Activity
16. Play Activity game
17. Exit back to main game scene
18. Press log out button
19. Use Website to check Bon's stats



# Bugs
Unity Scene Environment does not spawn after entering a scene that requires it more than once - verification on whether some functions that depend on the scene environment to work will have issues are unclear
Bon's food does not spawn again after feeding to bon once already per session

# Credits
## Content
- Code for activity was adapted from GMGStudio's tutorial on how to create a 2D fruit catching game
link: https://www.youtube.com/watch?v=CcL78URtpHE&list=PLB8RAOcCIoixo17b6tqLoa4JFp51GfZfO
- Code for Timer and sliders was adapted from CatoDevs tutorial on how to create a timer in unity
link: https://www.youtube.com/watch?v=-dKkAzCrBOY
- Code for Spawning bon when plane is detected adapted from Dilmer Valecillos's tutorial on spawning objects on plane detected
link: https://www.youtube.com/watch?v=oBKrdRI_NGI

## Acknowledgements
Inspiration for project is from tamagotchis and other similar pet-caring sims.
