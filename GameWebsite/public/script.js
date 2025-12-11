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
    
    .then(async (userCredential) => {

        const user = userCredential.user;
        const playerId = user.uid;   // 🎉 CORRECT — we use the Auth UID

        console.log("User created:", playerId);

        await createPlayer(playerId, name, email, password);

        localStorage.setItem("playerId", playerId);
        window.location.href = "home.html";
    })
    .catch((error) => {
        alert(error.message);
    });

    createPlayer(playerId, name, email, password);
    console.log("Player created:", name);

    localStorage.setItem("playerId", playerId);
});
}
// == INDEX PAGE CODE, LOG IN ==
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

// ===== Image paths for different stages =====
const stageImages = {
    1: "images/bon_stage1.png",
    2: "images/bon_stage2.png"
};

// ===== Update creature image based on stage =====
function updateCreatureImage(stage) {
    const img = document.getElementById("creatureImage");
    img.src = stageImages[stage] ?? "images/placeholder.png";
}

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
    location.reload(true);
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
                data: [
                    creature.hunger,
                    creature.cleanliness,
                    creature.happiness
                ],
                backgroundColor: [
                    "#ffb445ff",
                    "#9ffaf5ff",
                    "#b2ff6aff"
                ]
            }]
        },
        options: {
            indexAxis: 'y',
            responsive: true,
            scales: {
                x: {
                    min: 0,
                    max: 100,
                    grid: {
                        display: false
                    }
                },
                y: {
                    grid: {
                        display: false
                    }
                }
            },
            plugins: {
                legend: {
                    display: false
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
            window.location.href = "index.html";
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
        document.getElementById("stage").textContent = "Stage: " + data.creature.stage;

        // Update Chart
        updateCreatureImage(data.creature.stage);
        updateChart(data.creature);
    });
}

// == Logout button ==
document.getElementById("logOutBtn")?.addEventListener("click", () => {
    localStorage.removeItem("playerId");
    window.location.href = "index.html";
});
