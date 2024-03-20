<div class="col-7 ps-0 form-floating" id="searchUser">
    <input type="text" id="searchField" class="form-control me-5" placeholder="Caută utilizator" />
    <label for="searchField">
        <span class="material-symbols-outlined me-2 ms-1" style="vertical-align:middle;font-size:1.5rem;">
            search
        </span>Caută utilizator
    </label>
    <span class="material-symbols-outlined" style="vertical-align:middle;" id="clearSearchIcon">
        close
    </span>
</div>

var searchUsersInput = document.getElementById("searchField");
searchUsersInput.addEventListener("input", FilterUsers);

function FilterUsers() {
    var input = document.getElementById("searchField");
    var filter = input.value.toUpperCase();
    var table = document.getElementById("userTableBody");
    var rows = table.getElementsByTagName("tr");

    for (var i = 0; i < rows.length; i++) {
        var nameColumn = rows[i].getElementsByTagName("td")[1];
        var idColumn = rows[i].getElementsByTagName("td")[2];
        var name = nameColumn.textContent || nameColumn.innerText;
        var id = idColumn.textContent || idColumn.innerText;

        if (name.toUpperCase().indexOf(filter) > -1 || id.indexOf(filter) > -1) {
            rows[i].style.display = "";
        } else {
            rows[i].style.display = "none";
        }
    }
}
