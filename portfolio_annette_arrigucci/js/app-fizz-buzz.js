$(document).ready(function() {

	$("#submit-button-fizz-buzz").click(function(event) {
	    event.preventDefault();
	    $("#error-panel-fizz-buzz").empty();
		$("#fizz-buzz-result").empty();
		var myInput1 = $("#number-1-fizz-buzz").val();
		var myInput2 = $("#number-2-fizz-buzz").val();
		if(myInput1 == '' || myInput2 == ''){
		    $("#error-panel-fizz-buzz").append("Please enter two values<br>");
		}
		else if(validateNumber(myInput1) && validateNumber(myInput2)){
			var number1 = parseInt(myInput1);
			var number2 = parseInt(myInput2);
			fizzBuzz(number1, number2);
		}
	});

	function validateNumber(myInput) {
		if (+(myInput) % 1 != 0) {
		    $("#error-panel-fizz-buzz").append("Input must be an integer<br>");
			return false;
		}
		else if	((+(myInput) < 1) || (+(myInput) > 100)){
		    $("#error-panel-fizz-buzz").append("Input must be between 1 and 100<br>");
			return false;
		}
		else {
			return true;
		}
	}

	function fizzBuzz(myNumber1, myNumber2){
		for(var i=1; i<=100; i++){
			if(i % myNumber1 == 0){
				$("#fizz-buzz-result").append("Fizz");
			}
			if(i % myNumber2 == 0){
				$("#fizz-buzz-result").append("Buzz");
			}
			if(i % myNumber1 != 0 && i % myNumber2 != 0) {
				$("#fizz-buzz-result").append(i);
			}
			$("#fizz-buzz-result").append("<br>");
		}
	}
});