function catcheven() {
    const base_url = location.protocol + '//' + document.domain + ':' + location.port;
    function distric() {
        $('#tenTp').change(() => {
            var province = $('#tenTp').val();
            $.ajax({
                url: base_url+"/Home/GetDistrict?province=" + province,
                method: "POST",
                contentType: "html",
                success: (res) => {
                    $("#tenQuan").empty();
                    $("#tenQuan").append(res);
                }
            })
        })
    }
    function ward() {
        $("#tenTp, #tenQuan").change(() => {
            var province = $('#tenTp').val();
            var distric = $("#tenQuan").val();           
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
                    $("#tenPhuong").empty();
                    $("#tenPhuong").append(res);

                }
            })
        })

    }
    function street() {
        $("#tenTp, #tenQuan, #tenPhuong").change(()=>{
            var province = $('#tenTp').val();
            var distric = $("#tenQuan").val();
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
                    $("#tenDuong").empty();
                    $("#tenDuong").append(res);

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