	function Calc(a, b, Op) {
			if (Op == 1) {return a-b;}
			else if (Op == 2) {return a*b;}
			else if (Op == 3) { 
				if (b == 0) {alert("На 0 делить нельзя!");}
				else {return a/b;}}
			else {return a+b;} 
		}
		var k=Calc(5, 0, 5);
		alert (k);