$(document).ready(function() {

	$("#submit-button-palindrome").click(function(event) {
		event.preventDefault();
		$("#is-palindrome").empty();
		$("#error-panel-palindrome").empty();
		var myInput = $("#new-word-palindrome").val();
		if(myInput == ''){
		    $("#error-panel-palindrome").append("Please enter a word<br>");
		}
		else {
		    var letters = /^[a-zA-Z]+$/;
		    if (myInput.match(letters)) {
		        var myWord = myInput.toLowerCase();
		        var letterArray = myWord.split("");
		        if(checkPalindrome(letterArray) == true) {
		            $("#is-palindrome").append("Yes");
		        }
		        else {
		            $("#is-palindrome").append("No");
		        }
		    }
		    else {
		        $("#error-panel-palindrome").append("Please enter one word");
		    }
		}
	});

	function checkPalindrome(myLetterArray){
		var middle = 0;
		var isPalindrome = true; //flag if the word is a palindrome
		var end = myLetterArray.length - 1;
		if(myLetterArray.length % 2 == 1) {
			middle = parseInt((myLetterArray.length - 1)/2);
		}
		else {
			middle = myLetterArray.length / 2;
		}
		for(var i=0; i<middle; i++) {
			if(myLetterArray[i] !== myLetterArray[end - i]){
				isPalindrome = false;
			}
		}
		return isPalindrome;
	}

});