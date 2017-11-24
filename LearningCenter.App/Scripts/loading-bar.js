$(() => {
    $(document).on({
        ajaxStart: () => { $('#loadingElement').show(); },
        ajaxStop: () => { $('#loadingElement').fadeOut(); }
    });
});