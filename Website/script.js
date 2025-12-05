import { initializeApp } from "https://www.gstatic.com/firebasejs/12.6.0/firebase-app.js";
import { getAuth, createUserWithEmailAndPassword, signInWithEmailAndPassword } from "https://www.gstatic.com/firebasejs/12.6.0/firebase-auth.js";
import { getDatabase, child, push, ref, set, get } from "https://www.gstatic.com/firebasejs/12.6.0/firebase-database.js";
import firebaseConfig from "./firebase-config.js";

//Initialise Firebase
const app = initializeApp(firebaseConfig);
const db = getDatabase(app);

const players = ref(db, "players");
const auth = getAuth(app);


function createPlayer(playerId, name, email, password) {

    set(ref(db, 'players/' + playerId), {
        name: name,
        email: email,
        password: password,
        creature: {
            name: "BonNui",
            hunger: 100,
            cleanliness: 100,
            happiness: 100,
            stage: 1
        }
    });
}
// add event to button if button exists
if (document.getElementById("createPlayerBtn")){
    document.getElementById("createPlayerBtn").addEventListener("click", (event) => {
    event.preventDefault();
    
    const name = document.getElementById("playerName").value.trim();
    const password = document.getElementById("playerPassword").value.trim();
    const email = document.getElementById("playerEmail").value.trim();
    createUserWithEmailAndPassword(auth, email, password)
        .then((userCredential) => {
            // Signed in 
            const user = userCredential.user;
            console.log("User created:", user.uid);
        })
    const newPlayerRef = push(players);      // this generates a unique ID
    const playerId = newPlayerRef.key;  

    if (!name || !password || !email) {
        alert("Please enter a name and password.");
        return;
    }
    createPlayer(playerId, name, email, password);
    console.log("Player created:", name);

    localStorage.setItem("playerId", playerId);
    window.location.href = "home.html";
});
}

// add event to button if button exists
if (document.getElementById("logInBtn")){
    document.getElementById("logInBtn").addEventListener("click", (event) => {
    event.preventDefault();
    const email = document.getElementById("userEmail").value.trim();
    const password = document.getElementById("userPassword").value.trim();

    signInWithEmailAndPassword(auth, email, password)
    .then(async (userCredential) => {
        const user = userCredential.user;

        // to find the player entry with matching email
        const snapshot = await get(players);
        let playerId = null;

        snapshot.forEach(childSnap => { // iterate through each child
            // Check if the email matches
            if (childSnap.val().email === email) {
                playerId = childSnap.key;
            }
        });

        if (!playerId) {
            alert("Player data not found in database.");
            return;
        }

        localStorage.setItem("playerId", playerId);

        console.log("User logged in:", user.uid);
        window.location.href = "home.html";
    })
    .catch((error) => {
        console.error("Error logging in:", error.code, error.message);
        alert("Login failed. Please check your email and password.");
    });
});
}

//Home page log out button event
if (document.getElementById("logOutBtn")){

    document.getElementById("logOutBtn").addEventListener("click", (event) => {
        event.preventDefault();
        
        localStorage.removeItem("playerId"); // Clear stored player ID
        window.location.href = "landingPage.html";
});
}




//Get one player’s info
    // function getPLayerInfo(playerId) {
    //     const playerRef = child(players, playerId);

    //     get(playerRef).then(snapshot => {
    //         if (snapshot.exists()) {
    //             const data = snapshot.val();
    //             console.log("Selected Player:", playerId);
    //             //Update HTML
    //             document.getElementById("playerNameDisplay").textContent = data.name;
    //             document.getElementById("hunger").textContent = " Hunger: " + data.creature.hunger;
    //             document.getElementById("cleanliness").textContent = " Cleanliness: " + data.creature.cleanliness;
    //             document.getElementById("happiness").textContent = " Happiness: " + data.creature.happiness;
    //             document.getElementById("stage").textContent = " Stage: " + data.creature.stage;

    //         } else {
    //             console.log("Player not found");
    //         }
    //     }).catch(console.error);
    // }}
