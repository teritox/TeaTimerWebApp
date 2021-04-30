// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var intervalId = 0;
var time = "00:00";

function StartCountDown()
{
    if (intervalId == 0)
    {
        intervalId = setInterval(TickTime, 1000)
    }

    timer = document.getElementById('timer')

    time = timer.innerHTML
}

function TickTime() {
    var timer = document.getElementById('timer');

    console.log(timer)

    var minutes = timer.innerText.slice(0, 2)
    var seconds = timer.innerText.slice(3, 5)

    console.log(seconds)
    console.log(minutes)

    timer.innerText = "";

    if (seconds == "00")
    {

        seconds = "59";

        var first = minutes.slice(0, 1)
        var next = minutes.slice(1, 2)

        if (first == 0) {

            next--;

            if (next == 0) {

                timer.innerHTML = "00:" + seconds;

            }
            else {

                timer.innerHTML = "0" + next + ":" + seconds;

            }
        }
        else {
            minutes--;

            timer.innerHTML = minutes + ":" + seconds;
        }
    }
    else  
    {
        var first = seconds.slice(0, 1)
        var next = seconds.slice(1, 2)

        console.log(first)
        console.log(next)

        if (first == "0") {

            next--;

            if (minutes == "00" && next == "0")
            {
                    StopCountDown();
                document.getElementById('audio').play()
                timer.innerHTML = "00:00";
                
            }
            else if (next == 0) {

                timer.innerHTML = minutes + ":00";

            }
            else {

                timer.innerHTML = minutes + ":0" + next;

            }

        }
        else {

            seconds--;

            if (seconds == "9") {
                timer.innerHTML = minutes + ":0" + seconds;
            }
            else {
                timer.innerHTML = minutes + ":" + seconds;
            }
        }
    }
}

function StopCountDown()
{
    if (intervalId != 0)
    {
        clearInterval(intervalId)
        intervalId = 0;
    }
}

function ResetCountDown(brewTime)
{
    if (intervalId != 0)
    {
        clearInterval(intervalId)
        intervalId = 0;
    }

    timer = document.getElementById('timer')

    timer.innerHTML = "";

    timer.innerHTML = time;
}

