// Uzmanlık formu: canlı önizleme, hızlı seçim ve "bu isim zaten var" kontrolü
$(function () {
    var mevcut = (window.mevcutUzmanliklar || []).map(function (a) {
        return a.toLocaleLowerCase('tr-TR').trim();
    });

    // Models/UzmanlikHelper.cs ile aynı mantık
    function ikon(ad) {
        var a = (ad || '').toLocaleLowerCase('tr-TR');
        if (a.indexOf('küçük') > -1) return 'bi-house-heart';
        if (a.indexOf('egzotik') > -1) return 'bi-feather';
        if (a.indexOf('cerrahi') > -1) return 'bi-bandaid';
        if (a.indexOf('dermatoloji') > -1 || a.indexOf('deri') > -1) return 'bi-droplet-half';
        if (a.indexOf('kardiyoloji') > -1 || a.indexOf('kalp') > -1) return 'bi-heart-pulse';
        if (a.indexOf('ortopedi') > -1) return 'bi-person-walking';
        if (a.indexOf('oftalmoloji') > -1 || a.indexOf('göz') > -1) return 'bi-eye';
        if (a.indexOf('diş') > -1) return 'bi-emoji-smile';
        if (a.indexOf('onkoloji') > -1) return 'bi-shield-plus';
        if (a.indexOf('radyoloji') > -1 || a.indexOf('görüntüleme') > -1) return 'bi-activity';
        if (a.indexOf('dahiliye') > -1) return 'bi-clipboard2-pulse';
        return 'bi-award';
    }

    function zatenVar(ad) {
        return !!ad && mevcut.indexOf(ad.toLocaleLowerCase('tr-TR').trim()) > -1;
    }

    function guncelle() {
        var ad = $('#Ad').val().trim();

        $('#onizAd').text(ad || 'Uzmanlık alanı');
        $('#onizIkon').attr('class', 'bi ' + ikon(ad));

        var tekrar = zatenVar(ad);
        $('#adUyari').toggleClass('d-none', !tekrar);
        $('#kaydetButon').prop('disabled', tekrar);

        $('.quick-chip').each(function () {
            var oneri = String($(this).data('ad'));
            var kullanilmis = zatenVar(oneri);
            $(this)
                .prop('disabled', kullanilmis)
                .toggleClass('opacity-50', kullanilmis)
                .toggleClass('active', oneri === ad)
                .attr('title', kullanilmis ? 'Bu uzmanlık zaten ekli' : '');
        });
    }

    $('.quick-chip').on('click', function () {
        $('#Ad').val($(this).data('ad')).trigger('input');
    });

    $('#Ad').on('input', guncelle);
    guncelle();
});