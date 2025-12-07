import { initializeApp } from "https://www.gstatic.com/firebasejs/12.6.0/firebase-app.js";
import { getAuth, createUserWithEmailAndPassword, signInWithEmailAndPassword } from "https://www.gstatic.com/firebasejs/12.6.0/firebase-auth.js";
import { getDatabase, child, push, ref, set, get, onChildChanged } from "https://www.gstatic.com/firebasejs/12.6.0/firebase-database.js";
import firebaseConfig from "./firebase-config.js";

//Initialise Firebase
const app = initializeApp(firebaseConfig);
const db = getDatabase(app);

const players = ref(db, "players");
const auth = getAuth(app);



// == SIGN UP PAGE CODE, CREATE PLAYER ==
function createPlayer(playerId, name, email, password) {

    set(ref(db, 'players/' + playerId), {
        username: name,
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
// == LANDING PAGE CODE, LOG IN ==
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

// == HOME PAGE CODE ==
// ===== Get player data by ID =====
async function getPlayerById(playerId) {
    const playerRef = child(ref(db, "players"), playerId);
    const snapshot = await get(playerRef);
    return snapshot.exists() ? snapshot.val() : null;
}

const chosenPlayer = ref(db, "players/" + localStorage.getItem("playerId"));
onChildChanged(chosenPlayer, onPlayerChanged);

function onPlayerChanged(snapshot) {
    const playerData = snapshot.val();
   alert("Creature data updated!");
}


// ===== Chart.js setup =====
let chart = null;

function updateChart(creature) {
    const ctx = document.getElementById("statsChart");

    if (chart) chart.destroy();

    chart = new Chart(ctx, {
        type: "bar",
        data: {
            labels: ["Hunger", "Cleanliness", "Happiness"],
            datasets: [{
                label: "Bon Stats",
                data: [
                    creature.hunger,
                    creature.cleanliness,
                    creature.happiness
                ]
            }]
        },
        options: { 
            indexAxis: 'y',
            scales: {
            y: {
            beginAtZero: true
            }
            }
        }
    });
}

// == On Home Page Load ==
if (window.location.pathname.endsWith("home.html")) {

    window.addEventListener("load", async () => { 

        const playerId = localStorage.getItem("playerId");

        if (!playerId) {
            window.location.href = "landingPage.html";
            return;
        }

        // Fetch DB data
        const data = await getPlayerById(playerId);

        if (!data) {
            console.log("Player not found");
            return;
        }

        // Update text data
        document.getElementById("playerNameDisplay").textContent = data.username;
        document.getElementById("creatureName").textContent = data.creature.name;
        document.getElementById("hunger").textContent = "Hunger: " + data.creature.hunger;
        document.getElementById("cleanliness").textContent = "Cleanliness: " + data.creature.cleanliness;
        document.getElementById("happiness").textContent = "Happiness: " + data.creature.happiness;
        document.getElementById("stage").textContent = "Stage: " + data.creature.stage;

        // Update Chart
        updateChart(data.creature);
    });
}

// == Logout button ==
document.getElementById("logOutBtn")?.addEventListener("click", () => {
    localStorage.removeItem("playerId");
    window.location.href = "landingPage.html";
});