	function Sign(x) {
			if (x < 0) {return -1;}
			else if (x == 0) {return 0;}
			else {return 1;}
		}
		var k = Sign(-3);
		alert(k);