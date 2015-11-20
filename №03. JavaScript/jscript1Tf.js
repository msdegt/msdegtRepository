		function checkTest(){
		//var n = document.forms.test.elements;
		var s = document.forms.test.getElementsByTagName("input");
		var sum = 0;
		var sumPercent = 0;
		var t = 0;
		//alert(s[0]);
	
	for (var i=0; i < s.length; i++){
		if (s[i].type == "radio"){
			if(s[i].checked) {
				if(i==0 || i==6 || i==11){
					sum = sum + 1;                 
				}                   
			}
		}
		if (s[i].type == "checkbox"){
			if(s[i].checked) {
				if(i==13 || i==15 || i==16 || i==17){
					t = t+0.5;
				}else {
					t = t-0.5;
				}                   
			}
		}
	}
	
	sum = sum + Math.max(t,0);
     //alert (sum);

     sumPercent = (sum / 5) * 100;
		
		switch (true){
			case sumPercent < 25:
				alert("Совсем печально! Ваш результат: " + sum + " балла(ов), " + "что составило от общего числа правильных ответов: " + sumPercent + "%");
				break;
			case sumPercent < 50:
				alert("Плохо! Учите дальше. Ваш результат: " + sum + " балла(ов), " + "что составило от общего числа правильных ответов: " + sumPercent + "%");
				break;
			case sumPercent < 75:
				alert("Могло быть лучше! Ваш результат: " + sum + " балла(ов), " + "что составило от общего числа правильных ответов: " + sumPercent + "%");
				break;
			case sumPercent < 100:
				alert("Хорошо! Есть к чему стремиться! Ваш результат: " + sum + " балла(ов), " + "что составило от общего числа правильных ответов: " + sumPercent + "%");
				break;
			case sumPercent == 100:
				alert("Отлично! Продолжайте в том же духе! Ваш результат: " + sum + " балла(ов), " + "что составило от общего числа правильных ответов: " + sumPercent + "%");
				break;
			 default:
			 alert("Я таких значений не знаю!");
		}
	}
	