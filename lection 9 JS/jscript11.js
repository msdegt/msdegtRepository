	function RectP(x1, y1, x2, y2) {
		var a = Math.abs(x2-x1);
		var b = Math.abs(y2-y1);
		var p = (a+b)*2;
		return p;		
		}
		var k = RectP(9, 2, 3, 6);
		alert(k);