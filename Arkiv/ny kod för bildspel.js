
.dot - row {
    float: left;
    width: 100 %;
    margin - top: -5px;
}

.inner - dot - row {
    margin: 20px 0 0 42 %;
}

<div class="dot-row">
    <div class="inner-dot-row">
        <img id="one" src="~/images/blue-dot.webp" width="25px" />
        <img id="two" src="~/images/white-dot.webp" width="25px" />
        <img id="three" src="~/images/white-dot.webp" width="25px" />
        <img id="four" src="~/images/white-dot.webp" width="25px" />
    </div>
</div>

    function changeFrame(direction) {
        oldIndex = currentIndex;
        currentIndex += direction;

        // Gör så att frames loopar runt (både framåt och bakåt)
        if (currentIndex >= frames.length) {
            currentIndex = 0;
        }
        else if (currentIndex < 0) {
            currentIndex = frames.length - 1;
        }

        document.getElementById(frames[oldIndex]).classList.remove("visible");
        document.getElementById(frames[currentIndex]).classList.add("visible");

        if (oldIndex == 0){
            dot = document.getElementById("one");
            dot.innerHTML = "<img id='one' src='~/images/white-dot.webp' width='25px'/>";
        }
        else if (oldIndex == 1) {
            dot = document.getElementById("two");
            dot.innerHTML = "<img id='two' src='~/images/white-dot.webp' width='25px'/>";
        }

        if (currentIndex == 0) {
            dot = document.getElementById("one");
            dot.innerHTML = "<img id='one' src='~/images/blue-dot.webp' width='25px'/>";
        }
        else if (oldIndex == 1) {
            dot = document.getElementById("two");
            dot.innerHTML = "<img id='two' src='~/images/blue-dot.webp' width='25px'/>";
        }
    }


    function changeFrameFromDots(newDotId)
    {
        newDot = document.getElementById(newDotId);
        currentDot = document.getElementById(currentDotId);

        if (newDotId == currentDotId) {
            return;
        }
        else if (newDotId == "one") {
            newDot.innerHTML = "<img id='one' src='~/images/blue-dot.webp' onclick='changeFrameFromDots(\'one\')' width='25px'/>";            
            document.getElementById(frames[1]).classList.add("visible");
        }
        else if (newDotId == "two") {
            newDot.innerHTML = "<img id='two' src='~/images/blue-dot.webp' onclick='changeFrameFromDots('two')' width='25px'/>";
            document.getElementById(frames[2]).classList.add("visible");
        }
        else if (newDotId == "three") {
            newDot.innerHTML = "<img id='three' src='~/images/blue-dot.webp' onclick='changeFrameFromDots('three')' width='25px'/>";
            document.getElementById(frames[3]).classList.add("visible");
        }
        else if (newDotId == "four") {
            newDot.innerHTML = "<img id='four' src='~/images/blue-dot.webp' onclick='changeFrameFromDots('four')' width='25px'/>";
            document.getElementById(frames[3]).classList.add("visible");
        }

        currentDot.innerHTML = "<img id=" + currentDotId + " src='~/images/white-dot.webp' onclick='changeFrameFromDots(" + currentDotId + ")' width='25px'/>";
        
        if (currentDotId == "one")
            document.getElementById(frames[1]).classList.remove("visible");
        else if (currentDotId == "two")
            document.getElementById(frames[2]).classList.remove("visible");
        else if (currentDotId == "three")
            document.getElementById(frames[3]).classList.remove("visible");
        }
        else if (currentDotId == "four")
            document.getElementById(frames[4]).classList.remove("visible");

       currentDotId = newDotId;
    }