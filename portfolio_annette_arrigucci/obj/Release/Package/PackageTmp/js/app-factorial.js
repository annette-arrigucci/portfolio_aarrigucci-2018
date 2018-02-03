$(document).ready(function() {

	$("#submit-button-factorial").click(function(event) {
	    event.preventDefault();
	    $("#error-panel-factorial").empty();
		$("#factorial-result").empty();
		var myInput = $("#new-number-factorial").val();
		if (myInput == '') {
		    $("#error-panel-factorial").append("Please enter a non-negative integer<br>");
		}
        else if (+(myInput) % 1 != 0) {
			$("#error-panel-factorial").append("Input must be an integer<br>");
		}
		else if	(+(myInput) < 0){
		    $("#error-panel-factorial").append("Input must not be negative");
		}
		else {
			var number = parseInt(+(myInput));
			$("#factorial-result").append(factorial(number));
		}
	});

	function factorial(myNumber){
		var myProduct = myNumber;
		var i = myNumber;
		while(i > 1) {
			i--;
		 	myProduct*=i;
		}
		return myProduct;
	}

});