// =========================================================
// VetKlinik Admin - Ortak liste davranışları
// Kullanım: kartların kapsayıcısına data-liste="sayfaAdi",
// her karta class="liste-item" ve data-ara / data-durum / data-xxx (sıralama) verilir.
// =========================================================
$(function () {

    // ---------- Silme onay penceresi ----------
    $(document).on('click', '[data-sil-url]', function () {
        var $buton = $(this);
        var uyari = $buton.data('sil-uyari');

        $('#silModalForm').attr('action', $buton.data('sil-url'));
        $('#silModalAd').text($buton.data('sil-ad'));
        $('#silModalUyari').toggleClass('d-none', !uyari).text(uyari || '');
        $('#silModalButon').prop('disabled', !!uyari);

        bootstrap.Modal.getOrCreateInstance(document.getElementById('silModal')).show();
    });

    // ---------- Liste (arama / filtre / sıralama / görünüm) ----------
    var $liste = $('[data-liste]');
    if (!$liste.length) return;

    var $ogeler = $liste.children('.liste-item');
    var gorunumAnahtari = 'gorunum_' + $liste.data('liste');

    function uygula() {
        var aranan = ($('[data-liste-ara]').val() || '').toLocaleLowerCase('tr-TR').trim();
        var filtre = $('[data-liste-filtre]').val() || '';
        var gorunen = 0;

        $ogeler.each(function () {
            var $oge = $(this);
            var metin = String($oge.data('ara') || '').toLocaleLowerCase('tr-TR');
            var uygun = metin.indexOf(aranan) !== -1 && (!filtre || String($oge.data('durum')) === filtre);

            $oge.toggleClass('d-none', !uygun);
            if (uygun) gorunen++;
        });

        $('[data-liste-sayac]').text(gorunen);
        $('[data-liste-sonucyok]').toggleClass('d-none', $ogeler.length === 0 || gorunen > 0);
    }

    function sirala() {
        var deger = $('[data-liste-sirala]').val();
        if (!deger) return;

        var parca = deger.split('-');
        var alan = parca[0];
        var yon = parca[1] === 'desc' ? -1 : 1;

        var sirali = $ogeler.get().sort(function (a, b) {
            var x = $(a).data(alan), y = $(b).data(alan);
            if (typeof x === 'number' && typeof y === 'number') return (x - y) * yon;
            return String(x).localeCompare(String(y), 'tr') * yon;
        });
        $liste.append(sirali);
    }

    function gorunumAyarla(gorunum) {
        $liste.toggleClass('liste-gorunum', gorunum === 'list');
        $('[data-gorunum]').removeClass('active').filter('[data-gorunum="' + gorunum + '"]').addClass('active');
        try { localStorage.setItem(gorunumAnahtari, gorunum); } catch (e) { }
    }

    $('[data-liste-ara]').on('input', uygula);
    $('[data-liste-filtre]').on('change', uygula);
    $('[data-liste-sirala]').on('change', sirala);
    $('[data-gorunum]').on('click', function () { gorunumAyarla($(this).data('gorunum')); });
    $('[data-liste-temizle]').on('click', function () {
        $('[data-liste-ara]').val('');
        $('[data-liste-filtre]').val('');
        uygula();
    });

    var kayitliGorunum = 'grid';
    try { kayitliGorunum = localStorage.getItem(gorunumAnahtari) || 'grid'; } catch (e) { }
    gorunumAyarla(kayitliGorunum);
    sirala();
    uygula();
});