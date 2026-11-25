// Hizmet formu: canlı önizleme, hızlı süre seçimi ve karakter sayacı
$(function () {
    // Models/HizmetHelper.cs ile aynı mantık
    function ikon(ad) {
        var a = (ad || '').toLocaleLowerCase('tr-TR');
        if (a.indexOf('aşı') > -1) return 'bi-eyedropper';
        if (a.indexOf('kısır') > -1 || a.indexOf('ameliyat') > -1 || a.indexOf('cerrahi') > -1) return 'bi-bandaid';
        if (a.indexOf('diş') > -1) return 'bi-emoji-smile';
        if (a.indexOf('tıraş') > -1 || a.indexOf('bakım') > -1 || a.indexOf('banyo') > -1) return 'bi-scissors';
        if (a.indexOf('röntgen') > -1 || a.indexOf('ultrason') > -1 || a.indexOf('tahlil') > -1) return 'bi-activity';
        if (a.indexOf('muayene') > -1 || a.indexOf('kontrol') > -1) return 'bi-heart-pulse';
        return 'bi-clipboard2-pulse';
    }

    function sureMetni(dk) {
        if (!dk || dk <= 0) return '-- dk';
        if (dk < 60) return dk + ' dk';
        var saat = Math.floor(dk / 60), dakika = dk % 60;
        return dakika ? saat + ' sa ' + dakika + ' dk' : saat + ' saat';
    }

    function guncelle() {
        var ad = $('#Ad').val().trim();
        $('#onizAd').text(ad || 'Hizmet adı');
        $('#onizIkon').attr('class', 'bi ' + ikon(ad));

        var klinikSecili = $('#KlinikId').val();
        $('#onizKlinik').text(klinikSecili ? $('#KlinikId option:selected').text() : 'Klinik seçilmedi');

        var dk = parseInt($('#SureDakika').val(), 10);
        $('#onizSure').text(sureMetni(dk));
        $('.quick-chip').removeClass('active').filter('[data-sure="' + dk + '"]').addClass('active');

        var ucret = parseFloat(($('#Ucret').val() || '').replace(',', '.'));
        $('#onizUcret').text(isNaN(ucret)
            ? '0,00'
            : ucret.toLocaleString('tr-TR', { minimumFractionDigits: 2, maximumFractionDigits: 2 }));

        var aciklama = $('#Aciklama').val() || '';
        $('#onizAciklama')
            .text(aciklama.trim() || 'Açıklama eklenmemiş')
            .toggleClass('fst-italic', !aciklama.trim());
        $('#aciklamaSayac').text(aciklama.length);
    }

    $('.quick-chip').on('click', function () {
        $('#SureDakika').val($(this).data('sure')).trigger('input');
    });

    $('#KlinikId, #Ad, #SureDakika, #Ucret, #Aciklama').on('input change', guncelle);
    guncelle();
});