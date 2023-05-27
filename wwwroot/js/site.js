
// Assign an "contextmenu" event to div01:
document.getElementById("div01").addEventListener("contextmenu", myFunction);

// Prevent default context menu:
const div = document.getElementById("div01");
div.addEventListener("contextmenu", (e) => { e.preventDefault() });

// Show hidden content:
function myFunction() {
    const div = document.getElementById("myDiv");
    const div1 = document.getElementById("demo");
    const div001 = document.getElementById("div001");
    const div01 = document.getElementById("div01");
    div.style.display = "block";
    div1.style.display = "none";
    div01.style.color = "steelblue"
    div001.style.color = "black"

}

function myFunction1() {
    const div = document.getElementById("myDiv");
    const div1 = document.getElementById("demo");
    const div001 = document.getElementById("div001");
    const div01 = document.getElementById("div01");
    div.style.display = "none";
    div1.style.display = "block";
    div001.style.color = "steelblue"
    div01.style.color = "black";

}

function search() {
    // Get the input value from the search bar
    var input = document.getElementById("searchInput").value.toUpperCase();
    // Get the table rows
    var rows = document.getElementsByTagName("tr");
    // This for loop iterates through all the rows and hides those that don't match the search input
    for (var i = 0; i < rows.length; i++) {
        var name = rows[i].getElementsByTagName("td")[0];
        if (
            name) {
            var textValue = name.textContent || name.innerText;
            if (textValue.toUpperCase().indexOf(input) > -1) {
                rows[i].style.display = "";
            } else {
                rows[i].style.display = "none";
            }
        }
    }
}

// Get the modal
var modal = document.getElementById('id01');

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}