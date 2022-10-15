<template>
  <div>
    <h1>ادخل اسمك</h1>
    <input
      @keypress.enter="picke()"
      v-model="name"
      type="text"
      style="
        width: 250px;
        border-radius: 50px;
        text-align: center;
        font-size: 20px;
      "
    />
    <br>
    <span class="text-danger" v-if="$v.name.$invalid && $v.name.$dirty"
      >خطأ في المدخلات</span
    >
    <br />
    <b-form-checkbox
      id="checkbox-1"
      v-model="isSendEmailWanted"
      name="checkbox-1"
      :value="true"
      :unchecked-value="false"
    >
      اذا كنت ترغب بتلقي ايميل بالاسم اللي رح يطلع لك
    </b-form-checkbox>
    <button
      style="width: 150px; border-radius: 50px"
      type="button"
      class="btn btn-outline-dark mt-4 mb-4"
      @click="cleare()"
    >
      مسح
    </button>
    <button
      style="width: 150px; border-radius: 50px"
      type="button"
      class="btn btn-outline-dark mt-4 mb-4"
      @click="picke()"
    >
      اختر
    </button>
    <p>المتبقي في القرعة : {{ theRemainingNames }}</p>
    <p v-if="pickedName.length < 9 && pickedName.length != 0">طلع لك</p>
    <h2 class="text-danger">{{ pickedName }}</h2>

    <div v-if="isLoading" class="text-center">
      <b-spinner label="Spinning"></b-spinner>
    </div>
    <b-modal
      id="modal-center"
      ref="my-modal"
      centered
      title="تأكيد"
      hide-footer
      no-close-on-esc
      no-close-on-backdrop
      hide-header-close
    >
      <div style="text-align: center;">
        <p class="moo">الرجاء كتابة الايميل تبعك وتأكد منه لاهنت</p>
        <input
          @keypress.enter="picke()"
          v-model="email"
          placeholder="الرجاء كتابة الايميل هنا"
          type="email"
          style="
            width: 326px;
            border-radius: 4px;
            text-align: center;
            font-size: 20px;
            margin: 38px 0;
          "
        />

        <b-button
          :disabled="$v.email.$invalid"
          :class="{
            unvalid: $v.email.$invalid == true,
          }"
          variant="outline-warning"
          block
          @click="picke()"
          >تابع</b-button
        >
        <b-button class="mt-3" variant="outline-danger" block @click="hideModal"
          >إلغاء</b-button
        >
      </div>
    </b-modal>
  </div>
</template>

<script>
import { ger3ahApi } from "../Services/Ger3ahService";

const {
  required,
  // maxLength,
  // minLength,
  // requiredIf,
  email,
} = require("vuelidate/lib/validators");

export default {
  data() {
    return {
      name: "",
      theRemainingNames: "",
      pickedName: "",
      email: "",
      isSendEmailWanted: false,
      isLoading: false,
    };
  },
  validations: {
    name: { required },
    email: { required, email },
  },
  methods: {
    getAllNames() {
      ger3ahApi
        .getNumberOfNameLeft()
        .then((res) => {
          this.theRemainingNames = res.data.number;
        })
        .catch((e) => {
          this.theRemainingNames = e;
          console.log(e);
        });
    },
    picke() {
      if (!this.name) {
        this.pickedName = "!!" + "نسيت تكتب اسمك" + "!!";
        return;
      }

      if (this.isSendEmailWanted == true) {
        if (this.email == "") {
          this.$refs["my-modal"].show();
          return;
        }
      }
      this.$refs["my-modal"].hide();
      this.isLoading = true;
      ger3ahApi
        .pickAName(this.name, this.email)
        .then((res) => {
          this.pickedName = res.data.name;
          if (!this.pickedName) {
            if (
              res.data.errors ==
              "the name that was entered  does not exist in the system"
            ) {
              this.pickedName =
                "الاسم المدخل ليس موجود في القرعة الرجاء التأكد من الاسم ";
            }
            if (
              res.data.errors ==
              " the picer is Already Picked a Name so he cant Picke anther name"
            ) {
              this.pickedName =
                "قد اخترت اسم من قبل في هذه القرعة إذا كنت ناسية رح شف تاريخ القِرع وبتحصلة ";
            }
            if (res.data.errors == "something went relly wrong") {
              this.pickedName = "فيه مصيبة حصلة في النظام كلم فهد لاهنت";
            }
          }
          this.email = "";
          this.isLoading = false;
          this.getAllNames();
        })
        .catch((e) => {
          this.theRemainingNames = e;
          console.log(e);
        });
    },
    hideModal() {
      this.email = "";
      this.$refs["my-modal"].hide();
    },
    cleare() {
      this.getAllNames();
      this.name = "";
      this.pickedName = "";
    },
  },
  mounted() {
    this.getAllNames();
  },
};
</script>

<style scoped>
.moo {
  text-align: right !important;
}
.unvalid {
  color: rgba(0, 0, 0, 0.508) !important;
  background-color: transparent;
  border-color: rgba(0, 0, 0, 0.508) !important;
}
</style>