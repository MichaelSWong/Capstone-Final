// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code and Vue.

var app = new Vue({
    el: '#app',
    data() {
        return {
            LocalT: {
                Name: "",
                PlayerName: "",
                PlayerName1: "",
                PlayerName2: "",
                PlayerName3: "",
                PlayerName4: "",
                PlayerName5: "",
                PlayerName6: "",
                PlayerName7: "",
            }
        }
    },

});




// target elements with the "draggable" class
interact('#draggable')
    .draggable({
        // enable inertial throwing
        inertia: true,
        // keep the element within the area of it's parent
        restrict: {
            restriction: "parent",
            endOnly: true,
            elementRect: { top: 0, left: 0, bottom: 1, right: -10 }
        },
        // enable autoScroll
        autoScroll: true,

        // call this function on every dragmove event
        onmove: dragMoveListener,
        // call this function on every dragend event
        onend: function (event) {
            var textEl = event.target.querySelector('p');

            textEl && (textEl.textContent =
                'moved a distance of '
                + (Math.sqrt(Math.pow(event.pageX - event.x0, 2) +
                    Math.pow(event.pageY - event.y0, 2) | 0))
                    .toFixed(2) + 'px');
        }
    });

function dragMoveListener(event) {
    var target = event.target,
        // keep the dragged position in the data-x/data-y attributes
        x = (parseFloat(target.getAttribute('data-x')) || 0) + event.dx,
        y = (parseFloat(target.getAttribute('data-y')) || 0) + event.dy;

    // translate the element
    target.style.webkitTransform =
        target.style.transform =
        'translate(' + x + 'px, ' + y + 'px)';

    // update the posiion attributes
    target.setAttribute('data-x', x);
    target.setAttribute('data-y', y);
}

// this is used later in the resizing and gesture demos
//window.dragMoveListener = dragMoveListener;

// function populates the player generators with players
function addinputFields() {
    var number = document.getElementById("draggable1").value;
    var container1 = document.getElementById("container1");
    var container2 = document.getElementById("container2");
    var container3 = document.getElementById("container3");
    var container4 = document.getElementById("container4");
    let counter = 1;
    ///hack fix this function!!!
    while (container1.hasChildNodes() && container2.hasChildNodes() && container3.hasChildNodes() && container4.hasChildNodes()) {
        container1.removeChild(container1.lastChild),
            container2.removeChild(container2.lastChild),
            container3.removeChild(container3.lastChild),
            container4.removeChild(container4.lastChild);
    }
    var currentNumber = number;
    while (currentNumber >= 1) {

        //var container = document.createElement("div");

        for (i = 0; i < currentNumber; i++) {

            var input = document.createElement("input");
            //var html = \"<div class=\"row1\">player<\/div>\";

            input.type = "text" + (i + 1);
            input.class = "row" + (counter);
            input.placeholder = "Player";
            input.setAttribute('v-model', 'LocalT.PlayerName' + (i + 1));

            if (input.class == "row1") {
                container1.appendChild(input);
                container1.appendChild(document.createElement("br"));
            }
            else if (input.class == "row2") {
                container2.appendChild(input);
                container2.appendChild(document.createElement("br"));
            }
            else if (input.class == "row3") {
                container3.appendChild(input);
                container3.appendChild(document.createElement("br"));
            }
            else if (input.class == "row4") {
                container4.appendChild(input);
                container4.appendChild(document.createElement("br"));
            }

        }
        currentNumber = currentNumber / 2;
        counter++;
    }
    //for (i = 0; i < number/2; i++) {

    //    var input = document.createElement("input");

    //    input.type = "text" + (i + 1);
    //    input.id = "draggable";
    //    input.placeholder = "Player " + (i + 1);  

    //    container2.appendChild(input);
    //    container2.appendChild(document.createElement("br"));
    //}
    //for (i = 0; i < number / 4; i++) {

    //    var input = document.createElement("input");

    //    input.type = "text" + (i + 1);
    //    input.id = "draggable";
    //    input.placeholder = "Player " + (i + 1);

    //    container3.appendChild(input);
    //    container3.appendChild(document.createElement("br"));
    //}
}


//$(document).ready(function () {
//    var max_fields = 10;
//    var wrapper = $(".container1");
//    var add_button = $(".add_form_field");

//    var x = 1;
//    $(add_button).click(function (e) {
//        e.preventDefault();
//        if (x < max_fields) {
//            x++;
//            $(wrapper).append('<div><input type="text" name="mytext[]"/><a href="#" class="delete">Delete</a></div>'); //add input box
//        }
//        else {
//            alert('You Reached the limits')
//        }
//    });

//    $(wrapper).on("click", ".delete", function (e) {
//        e.preventDefault(); $(this).parent('div').remove(); x--;
//    })
//});

// TEXT BOXES
$(document).ready(function () {

    var counter = 2;

    $("#addButton").click(function () {

        if (counter > 10) {
            alert("Only 10 textboxes allow");
            return false;
        }

        var newTextBoxDiv = $(document.createElement('div'))
            .attr("id", 'TextBoxDiv' + counter);

        newTextBoxDiv.after().html('<label>Textbox #' + counter + ' : </label>' +
            '<input type="text" name="textbox' + counter +
            '" id="textbox' + counter + '" value="" >');

        newTextBoxDiv.appendTo("#TextBoxesGroup");


        counter++;
    });

    $("#removeButton").click(function () {
        if (counter == 1) {
            alert("No more textbox to remove");
            return false;
        }

        counter--;

        $("#TextBoxDiv" + counter).remove();

    });

    $("#getButtonValue").click(function () {

        var msg = '';
        for (i = 1; i < counter; i++) {
            msg += "\n Textbox #" + i + " : " + $('#textbox' + i).val();
        }
        alert(msg);
    });
});

function GetHighestSlotID() {
    let foundSlots = $(".SlotsContainer .Slot");
    let highest = -1;
    for (let i = 0; i < foundSlots.length; i++) {
        let idText = foundSlots[i].id.substr(5);
        if (idText > highest) {

            highest = idText;
        }
    }

    return highest;
}
function CreateSlot() {
    let highestID = GetHighestSlotID();
    let slotsContainer = document.getElementsByClassName("SlotsContainer");

    let newElement = document.createElement("div");
    newElement.id = "draggable";
    slotsContainer.appendChild(newElement);

    newElement = newElement.createElement("div");
    newElement.id = "slot_" + (highestID + 1);
    slotsContainer.appendChild(newElement);

    let newContentElement = newElement.createElement("span");
}
function ConnectSlot(thisSlot) {
    console.log(thisSlot);

    let hasClicked = false;
    while (!hasClicked) {
        $(document).on("click", function (event) {
            hasClicked = true;
        });
    }
}

function GetNeededSlots(playerCount) {

    let neededSlots = 2;
    while (true) {
        if (playerCount > neededSlots) {
            neededSlots *= 2;
        }
        else {
            break;
        }
    }

    return neededSlots;
}

function CreateTeamsArray(str) {
    if (str != null) {
        let strArray = String(str).split(' ');

        let hasNull = false;
        for (let i = 0; i < strArray.length; i++) {
            if (strArray[i] == "null") {
                hasNull = true;
                break;
            }
        }

        let masterArray = [];
        let neededSlots = GetNeededSlots(strArray.length);
        let currStrI = 0;
        if (!hasNull && strArray.length != neededSlots) {
            for (let i = 0; i < neededSlots / 2; i++) {
                if (strArray.length > currStrI) masterArray.push([strArray[currStrI], null]);
                else masterArray.push([null, null]);
                currStrI++;
            }
            for (let i = 0; i < neededSlots / 2; i++) {
                if (strArray.length > currStrI) masterArray[i][1] = strArray[currStrI];
                currStrI++;
            }
        }
        else {
            let array = [];
            let first = true;
            for (let i = 0; i < strArray.length; i++) {
                if (strArray[i] == "null") strArray[i] = null;
                if (first) {
                    array.push(strArray[i]);
                }
                else {
                    array.push(strArray[i]);
                    masterArray.push(array);
                    array = [];
                }

                first = !first
            }
        }
        
        return masterArray;
    }
}
function CreateScoresArray(str) {
    if (str != null) {
        let strArray = String(str).split(' ');

        let masterArray = [];
        let array = [];
        let first = true;
        for (let i = 0; i < strArray.length; i++) {
            if (first) {
                array.push(Number(strArray[i]));
            }
            else {
                array.push(Number(strArray[i]));
                masterArray.push(array);
                array = [];
            }

            first = !first
        }

        return masterArray;
    }
}
function CreateFreshScoresArray(playerCount) {
    let neededSlots = GetNeededSlots(playerCount) / 2;

    let masterArray = [];
    let array = [];
    let currSlotCount = neededSlots;
    while (currSlotCount >= 1) {
        for (let i = 0; i < currSlotCount; i++) {
            array.push([0, 0]);
        }

        masterArray.push(array);
        array = [];
        currSlotCount /= 2;
    }

    return masterArray;
}

function DoubleArrayToString(array) {
    console.log(array);

    let str = "";
    for (let i = 0; i < array.length; i++) {
        for (let ii = 0; ii < array[i].length; ii++) {
            if (str == "") {
                str += array[i][ii];
            }
            else {
                str += " " + array[i][ii];
            }
        }
    }

    console.log(str);
    return str;
}