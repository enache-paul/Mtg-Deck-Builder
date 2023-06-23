// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const init = () => { 
    fillInManaCost();
    fillInRarity();
 }

document.addEventListener("DOMContentLoaded", init);

//TODO: Remove this function ones I figure out how to import modules
const getData = async(endpoint) => {

    const response = await fetch(endpoint, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => {
            return data;
        })
        .catch((error) => {
        console.error('Error:', error);
    });

    return await response;
}

const fillInManaCost = async() => {
    
    const highestManaCost = await getData("http://localhost:5280/cards/manaCosts");

    const newOption = document.createElement('option');
    newOption.value = "Any";
    newOption.textContent = "Any";

    document.querySelector(".mana-cost-filter").append(newOption);

    console.log(highestManaCost);

    for(i = 1; i <= parseInt(highestManaCost); i++) {

        console.log("Calling", i);
        const newOption = document.createElement('option');
        newOption.value = i;
        newOption.textContent = i;

        document.querySelector(".mana-cost-filter").append(newOption);
    }
}

const fillInRarity = async() => {
    
    const rarities = await getData("http://localhost:5280/cards/rarities");

    const newOption = document.createElement('option');
    newOption.value = "Any";
    newOption.textContent = "Any";

    document.querySelector(".rarity-filter").append(newOption);

    rarities.forEach(el => {
        const newOption = document.createElement('option');
        newOption.value = el.charAt(0);
        newOption.textContent = el;

        document.querySelector(".rarity-filter").append(newOption);
    })
}