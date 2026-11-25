// Müsaitlik formu: toplu gün seçimi ve hazır saat aralıkları
$(function () {
    $('[data-gun-sec]').on('click', function () {
        var secim = $(this).data('gun-sec'); // haftaici | hepsi | temizle

        $('input[name=gunler]').each(function () {
            var gun = parseInt($(this).val(), 10); // 0 = Pazar, 1 = Pazartesi ... 6 = Cumartesi
            var isaretle = secim === 'hepsi' || (secim === 'haftaici' && gun >= 1 && gun <= 5);
            $(this).prop('checked', isaretle);
        });
        $('#gunUyari').addClass('d-none');
    });

    $('[data-aralik]').on('click', function () {
        var parca = String($(this).data('aralik')).split('-');
        $('#baslangicSaati').val(parca[0]);
        $('#bitisSaati').val(parca[1]);
    });

    $('#musaitlikForm').on('submit', function (e) {
        if (!$('input[name=gunler]:checked').length) {
            e.preventDefault();
            $('#gunUyari').removeClass('d-none');
        }
    });

    $('input[name=gunler]').on('change', function () {
        $('#gunUyari').addClass('d-none');
    });
});