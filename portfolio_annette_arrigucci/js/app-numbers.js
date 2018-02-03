$(document).ready(function() {
	var numbersArray = [];

	$("#submit-button-numbers").click(function(event) {
	    event.preventDefault();
	    $("#error-panel-numbers").empty();
		var myInput = $("#new-number-numbers").val();
		if (myInput == '') {
		    $("#error-panel-numbers").append("Please enter an integer<br>");
		}
        else if (+(myInput) % 1 != 0) {
            $("#error-panel-numbers").append("Input must be an integer<br>");
		}
		else {
			var number = parseInt(+(myInput));
			addToArray(number);
			$("#new-number-numbers").val('');
		}
	});

	function addToArray(myNumber) {
		numbersArray.push(myNumber);
		$("#current-numbers").append(myNumber + "<br>");
	}

	function checkLength() {
	    if (numbersArray.length < 5) {
	        $("#error-panel-numbers").empty();
	        $("#error-panel-numbers").append("Please enter at least five numbers<br>");
	        return false;
	    }
	    else {
	        return true;
	    }
	}

	$("#least-button").click(function(event) {
		if(checkLength()) {
		    $("#least-display").text('');
			$("#least-display").text("     " + least());
		}
	});

	$("#greatest-button").click(function(event) {
	    if (checkLength()) {
		    $("#greatest-display").text('');
		    $("#greatest-display").text("     " + greatest());
		}
	});

	$("#mean-button").click(function(event) {
	    if (checkLength()) {
		    $("#mean-display").text('');
		    $("#mean-display").text("     " + mean());
		}
	});

	$("#sum-button").click(function(event) {
	    if (checkLength()) {
		    $("#sum-display").text('');
		    $("#sum-display").text("     " + sum());
		}
	});

	$("#product-button").click(function(event) {
	    if (checkLength()) {
		    $("#product-display").text('');
		    $("#product-display").text("     " + product());
		}
	});

	$("#clear-button-numbers").click(function (event) {
	    numbersArray = [];
	    $("#error-panel-numbers").empty();
	    $("#current-numbers").text('');
	    $("#least-display").text('');
	    $("#greatest-display").text('');
	    $("#mean-display").text('');
	    $("#sum-display").text('');
	    $("#product-display").text('');
	});

	function least() {
		var myLeast = numbersArray[0];
		for(var i=1; i<numbersArray.length; i++)
		{
			if (myLeast > numbersArray[i]){
				myLeast = numbersArray[i];
			}
		}
		return myLeast;
	}

	function greatest() {
		var myGreatest = numbersArray[0];
		for(var i=1; i<numbersArray.length; i++)
		{
			if (myGreatest < numbersArray[i]){
				myGreatest = numbersArray[i];
			}
		}
		return myGreatest;
	}

	function mean() {
		var myMean = sum() / numbersArray.length;
		return myMean;
	}

	function sum() {
		var mySum = 0;
		for(var i=0; i<numbersArray.length; i++){
			mySum+=numbersArray[i];
		}
		return mySum;
	}

	function product() {
		var myProduct = 1;
		for(var i=0; i<numbersArray.length; i++){
			myProduct*=numbersArray[i];
		}
		return myProduct;
	}
});