import { initializeApp } from "https://www.gstatic.com/firebasejs/12.6.0/firebase-app.js";
import { getAnalytics } from "https://www.gstatic.com/firebasejs/12.6.0/firebase-analytics.js";

const firebaseConfig = {
apiKey: "AIzaSyAwrcWvZVKZU9nDh7EEuRUzJirreFzrlX0",
authDomain: "itdddaprojecthe.firebaseapp.com",
projectId: "itdddaprojecthe",
storageBucket: "itdddaprojecthe.firebasestorage.app",
messagingSenderId: "724345577244",
appId: "1:724345577244:web:534f78e34a5bbc8a7ca4d9",
measurementId: "G-TFDH22JNWV"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);
const analytics = getAnalytics(app);

import { getDatabase, ref, child, set} from "https://www.gstatic.com/firebasejs/12.6.0/firebase-database.js";

const db = getDatabase(app);
const players = ref(db, 'players');

function createPlayerData(username, password) {
    
    console.log("Creating player data...");
    const player = child(players, username);

    set(player, { username, password }).then(() => {
        console.log("Data saved successfully.");
    }).catch((error) => {
        console.error("Error saving data: ", error);
    });
}
usernameInput = document.getElementById("usernameInput").value;
passwordInput = document.getElementById("passwordInput").value;
createPlayerData(usernameInput, passwordInput);