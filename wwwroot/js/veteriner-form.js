// Veteriner formu: hizmetleri kliniğe göre filtreler, canlı önizleme, şifre göster/gizle
$(function () {
    function hizmetleriFiltrele() {
        var klinikId = $('#KlinikId').val();

        $('.hizmet-secim').each(function () {
            var uygun = !!klinikId && String($(this).data('klinik')) === klinikId;
            $(this).toggleClass('d-none', !uygun);
            if (!uygun) $(this).find('input').prop('checked', false);
        });

        var gorunen = $('.hizmet-secim:not(.d-none)').length;
        $('#hizmetBilgi')
            .toggleClass('d-none', gorunen > 0)
            .text(klinikId ? 'Seçilen klinikte henüz hizmet tanımlanmamış.' : 'Hizmetleri görmek için önce klinik seçin.');

        onizle();
    }

    function onizle() {
        var ad = $('#Ad').val().trim();
        var soyad = $('#Soyad').val().trim();

        var basHarf = ((ad.charAt(0) || '') + (soyad.charAt(0) || '')).toLocaleUpperCase('tr-TR');
        $('#onizAvatar').text(basHarf || '?');
        $('#onizAd').text(ad || soyad ? 'Vet. Hek. ' + ad + ' ' + soyad : 'Veteriner adı');
        $('#onizEmail').text($('#Email').val().trim() || 'e-posta adresi');
        $('#onizKlinik').text($('#KlinikId').val() ? $('#KlinikId option:selected').text() : 'Klinik seçilmedi');

        var $uzmanlik = $('#onizUzmanlik').empty();
        $('input[name=SeciliUzmanlikIds]:checked').each(function () {
            $('<span class="chip blue"></span>').text($(this).data('ad')).appendTo($uzmanlik);
        });
        if (!$uzmanlik.children().length) {
            $uzmanlik.append('<span class="text-muted small">Uzmanlık seçilmedi</span>');
        }

        $('#onizUzmanlikSayi').text($('input[name=SeciliUzmanlikIds]:checked').length);
        $('#onizHizmetSayi').text($('input[name=SeciliHizmetIds]:checked').length);
    }

    $('#KlinikId').on('change', hizmetleriFiltrele);
    $('#Ad, #Soyad, #Email').on('input', onizle);
    $(document).on('change', 'input[name=SeciliUzmanlikIds], input[name=SeciliHizmetIds]', onizle);

    $('#sifreGoster').on('click', function () {
        var $sifre = $('#Sifre');
        var gizli = $sifre.attr('type') === 'password';
        $sifre.attr('type', gizli ? 'text' : 'password');
        $(this).find('i').toggleClass('bi-eye bi-eye-slash');
    });

    hizmetleriFiltrele();
});