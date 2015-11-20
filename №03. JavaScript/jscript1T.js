		function check(m,x1){	
			if ( m[x1].checked){
				return 1;
			}else {
				return 0;
			} 
		}
		
		function check2(m,x1,x2){
              var t = 0;
             for(i = 0; i < m.length; i++) {                
               if ( m[i].checked == true ){
                if (i == x1 || i==x2) {t = t + 0.5;}
                 else {t = t - 0.5;}
               }
              }
           return Math.max(t,0); 
         }
		
		function checkTest(){
		//var a = document.forms.test.elements;
		var a = document.getElementsByName("t1");
		var b = document.getElementsByName("t2");
		var c = document.getElementsByName("t3");
		var d = document.getElementsByName("t4");
		var e = document.getElementsByName("t5");
		var sum = 0;
		var sumPercent = 0;
		var t = 0;
		
	sum = sum + check(a,0);
	sum = sum + check(b,2);
	sum = sum + check(c,3);	
		
	/////////////////////////	
	 sum = sum + check2(d,1,3);

     /////////////////////

     sum = sum + check2(e,0,1);

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
	