       var dateErrorMes;
       
       function checkDate(yearStr, monthStr, dayStr)
      {
        dateErrorMes = "";
        if (yearStr != parseInt(yearStr)) {
           dateErrorMes = "Turi buti skaicius";
        return false;
        }
   
        year = parseInt(yearStr);
        month = parseInt(monthStr)-1; // Sausis - 0
        day = parseInt(dayStr);
        if (month < 0 || month > 11) {
           dateErrorMes = "Menuo turi buti tarp 1 ir 12";
           return false;
        }
        var date = new Date(year, month, day);
        if (date.getDate() != day) {
            dateErrorMes = "Diena neteisinga";
            return false;
        }
        return true;
       }

       
       function analizeDate(date)
       {
       
          var reg = /^([1-9][0-9]{3}).([0-9]{2}).([0-9]{2})$/;
          if (reg.test(date))
          {
             
             retArr = reg.exec(date);
             var ret = checkDate(retArr[1], retArr[2], retArr[3]);
             return ret;
          }
          else
          {
             dateErrorMes = "Neteisingas datos formatas"
             return false;
          }
       }
       
       function runCommand(command)
       {
          var reg = /\s*(show|hide)\s*\(?(\w+)\)?/;
          
          if (reg.test(command))
          {
             arrRet2 = reg.exec(command);
             arrRet = command.split(/\(/);
             
             retStr = command.replace(reg, "On object $2 was executed $1 command (ignored text in begining: $`, in end: $')");
             
             
             document.getElementById("LastCommand").firstChild.nodeValue = retStr;
             if (arrRet[0] == "show")
             {
                elem = document.getElementById(arrRet2[2]);
                elem.style.display = "inline";
             } else {
                elem = document.getElementById(arrRet2[2]);
                elem.style.display = "none";
             }
             
          }
       }
       
          function onClick(sender)
          {
             var textBoxMust = document.getElementById("Text1");
             var wasError = false;
             var errorsList = document.getElementById("errors");
             for (var i = errorsList.childNodes.length - 1; i >= 0; i--)
             {
                errorsList.removeChild(errorsList.childNodes[i]);
             }             
             if (textBoxMust.value == "")
             {
                wasError = true;
                var newLi = document.createElement("li");             
                newLi.appendChild(document.createTextNode("Pirmas laukas būtinai turi būti užpildytas"));
                errorsList.appendChild(newLi);

             } else {
                runCommand(textBoxMust.value);
             }
             var reg = /^\d+$/;
             
             var textBoxOnlyDigits = document.getElementById("Text2");
             
             
             if (!reg.test(textBoxOnlyDigits.value))
             {
                 wasError = true;
                var newLi = document.createElement("li");             
                newLi.appendChild(document.createTextNode("Antras laukas tik is skaiciu"));
                errorsList.appendChild(newLi);
                 
             }
             var textBoxDate = document.getElementById("Text3");
             
             if (!analizeDate(textBoxDate.value))
             {
                 wasError = true;
                var newLi = document.createElement("li");             
                newLi.appendChild(document.createTextNode(dateErrorMes));
                errorsList.appendChild(newLi);             
             }
             var status = document.getElementById("Status");
             if (wasError)
             {
                status.firstChild.nodeValue = "Error";
                status.style.color = "blue";
                status.style.fontSize = "smaller";
             }
             else
             {
                status.firstChild.nodeValue = "No errors";
                status.style.color = "green";
                
                
             }
             
          }
          
