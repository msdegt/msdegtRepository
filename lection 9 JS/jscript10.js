	function Pow(x, n) {
		var a = x;
			for (var i = 1; i < n; i++){
				a = a*x;	
			}
		return a;
		}
		var k = Pow(3, 3);
		alert(k);