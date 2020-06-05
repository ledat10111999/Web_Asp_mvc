function catcheven() {
    const base_url = location.protocol + '//' + document.domain + ':' + location.port;
    function distric() {
       
        $('#provinces').change(() => {
            var province = $('#provinces').val();
            $.ajax({
                url: base_url+"/Home/GetDistrict?province=" + province,
                method: "POST",

                contentType: "html",
                success: (res) => {

                    $("#districts").empty();
                    $("#districts").append(res);

                }
            })
        })
    }
    function ward() {
        $("#provinces, #districts").change(() => {
            var province = $('#provinces').val();
            var distric = $("#districts").val();
            
            var ward = {
                district: distric,
                province: province
            }
            $.ajax({
                url: base_url+ "/Home/GetWard",
                method: "POST",
                data: ward,
                dataType: "html",
                success: (res) => {
                    $("#wards").empty();
                    $("#wards").append(res);

                }
            })
        })

    }
    function street() {
        $("#provinces, #districts, #wards").change(()=>{
            var province = $('#provinces').val();
            var distric = $("#districts").val();
            var street = {
                district: distric,
                province: province
            }
            $.ajax({
                url: base_url + "/Home/GetStreet",
                method: "POST",
                data: street,
                dataType: "html",
                success: (res) => {
                    $("#streets").empty();
                    $("#streets").append(res);

                }
            })
        })
        
    }
    distric();
    ward();
    street();
}

$(document).ready(() => {
    new catcheven();


})