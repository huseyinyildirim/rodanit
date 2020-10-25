MailKontrol = function () { for (var a = arguments[0], c = "1234567890abcdefghijklmnoprstuvyzqwx[].+@-_ABCDEFGHIJKLMNOPRSTUVYZQWX", d = /(@.*@)|(\.\.)|(^\.)|(^@)|(@$)|(\.$)|(@\.)/, e = /^.+\@(\[?)[a-zA-Z0-9\-\.]+\.([a-zA-Z]{2,8}|[0-9]{1,3})(\]?)$/, b = 0; b < a.length; b++) if (c.indexOf(a.charAt(b)) < 0) return false; if (!a.match(d) && a.match(e)) return -1 }
function KucukHarf(a) { !a.val() == "" && a.val(a.val().toLowerCase()) }
function BuyukHarf(a) { !a.val() == "" && a.val(a.val().toUpperCase()) }
function SayisalKarakter(a) { if (!a.val() == "") if (!a.val().match(/^[\-0-9\s]+$/g)) { a.val(a.val().replace(/[^\-0-9\s]*/g, "")); } }

function menusec(menuadi) {
    $(".menualt div").css("display", "none")
    $(".menu ul li").removeClass("secili")
    $(".menu ul #" + menuadi + "").addClass("secili")
    $(".menualt #" + menuadi + "_detay").css("display", "inherit")
}

$(document).ready(function () {
    $('.onay').jConfirmAction({ question: "Eminmisiniz?", yesAnswer: "Evet", cancelAnswer: "Hayır" });
});

function HepsiniSec(divId) {
    var elmnts = document.getElementById(divId).getElementsByTagName('input');
    for (var i = 0; i < elmnts.length; i++) {
        if (elmnts[i].id.indexOf('chk') == 0) {
            if (elmnts[i].checked == false) {
                elmnts[i].checked = true;
            }
        }
    }
}

function HepsiniTemizle(divId) {
    var elmnts = document.getElementById(divId).getElementsByTagName('input');
    for (var i = 0; i < elmnts.length; i++) {
        if (elmnts[i].id.indexOf('chk') == 0) {
            if (elmnts[i].checked == true) {
                elmnts[i].checked = false;
            }
        }
    }
}

function coklusec(Val) {
    var ValChecked = Val.checked;
    var ValId = Val.id;
    var frm = document.forms[0];
    for (i = 0; i < frm.length; i++) {
        if (this != null) {
            if (ValId.indexOf('hepsinisec') != -1) {
                if (ValChecked)
                    frm.elements[i].checked = true;
                else
                    frm.elements[i].checked = false;
            }
            else if (ValId.indexOf('secim') != -1) {
                if (frm.elements[i].checked == false)
                    frm.elements[1].checked = false;
            }
        }
    }
}