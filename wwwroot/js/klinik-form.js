// Klinik formu: yazdıkça önizleme kartını ve çalışma süresini günceller
$(function () {
    function dakikaya(saat) {
        if (!saat) return null;
        var p = saat.split(':');
        return parseInt(p[0], 10) * 60 + parseInt(p[1], 10);
    }

    function guncelle() {
        var ad = $('#Ad').val().trim();
        var adres = $('#Adres').val().trim();
        var telefon = $('#Telefon').val().trim();
        var acilis = $('#AcilisSaati').val();
        var kapanis = $('#KapanisSaati').val();

        $('#onizAd').text(ad || 'Klinik adı');
        $('#onizAdres').text(adres || 'Adres bilgisi');
        $('#onizTelefon').text(telefon || 'Telefon eklenmemiş').toggleClass('text-muted', !telefon);
        $('#onizSaat').text((acilis ? acilis.substring(0, 5) : '--:--') + ' – ' + (kapanis ? kapanis.substring(0, 5) : '--:--'));

        var bas = dakikaya(acilis), bit = dakikaya(kapanis);
        var $sure = $('#calismaSuresi');

        if (bas !== null && bit !== null && bit > bas) {
            var fark = bit - bas;
            var saat = Math.floor(fark / 60), dakika = fark % 60;
            $sure.text(saat + ' saat' + (dakika ? ' ' + dakika + ' dk' : '')).removeClass('text-danger');

            var simdi = new Date();
            var suAn = simdi.getHours() * 60 + simdi.getMinutes();
            var acik = suAn >= bas && suAn < bit;
            $('#onizDurum')
                .attr('class', 'chip' + (acik ? '' : ' gray'))
                .html('<span class="dot"></span>' + (acik ? 'Açık' : 'Kapalı'));
        } else {
            $sure.text('Kapanış saati açılıştan sonra olmalı').addClass('text-danger');
        }
    }

    $('#Ad, #Adres, #Telefon, #AcilisSaati, #KapanisSaati').on('input change', guncelle);
    guncelle();
});