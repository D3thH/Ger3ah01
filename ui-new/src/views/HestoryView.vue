<template>
  <div class="about">
    <div v-if="isVisible" >
      <b-icon  icon="exclamation-circle-fill" variant="danger"></b-icon>
      <p class="text-danger" style="font-size: small">
      ملاحظة : هذة النافذة محطوطة للمساعدة في حال احد نسي اللي عندة فقط وفقط!
      الرجاء عدم استخدامها لغير هذا الغرض ( بالعربي لا تخرب وشكرا -_- ) 
    </p>
    </div>
    <div v-else>
      <h1>ادخل اسمك</h1>
    <input
      style="
        width: 250px;
        border-radius: 50px;
        text-align: center;
        font-size: 20px;
      "
      v-model="name"
      type="text"
      @keypress.enter="hestory()"
    />
    <br />
    <button
      style="width: 150px; border-radius: 50px"
      type="button"
      class="btn btn-outline-dark mt-4 mb-4"
      @click="hestory()"
    >
      الاسماء اللي طلعوا لي
    </button>
    <b-table
      v-if="hestoryNames.length > 0"
      striped
      hover
      style="
      overflow-y: auto;
      max-height: 200px;"
      :sticky-header=true
      :items="hestoryNames"
      :fields="fields"
    ></b-table>
    <div v-if="isLoading"  class="text-center">
      <b-spinner label="Spinning"></b-spinner>
    </div>
    <br>
    {{massage}}
    </div>
  </div>
</template>

<script>
import {ger3ahApi} from '../Services/Ger3ahService'

export default {
  data() {
    return {
      name: "",
      hestoryNames: [],
      isVisible: true,
      isLoading: false,
      fields: [
        {
          key: "الاسم",
          sortable: true,
        },
        {
          key: "التاريخ",
          sortable: false,
        },
        {
          key: "الوقت",
          sortable: false,
        },
      ],
      massage: ''
    };
  },
  methods: {
    hestory() {
      this.hestoryNames = [];
      this.massage = '';
      this.isLoading = true;
      ger3ahApi.getGer3ahHestory(this.name)
        .then((res) => {
          if (res.data.result.length == 0) {
            this.massage = 'ما فيه تنائج' ;
          }
          res.data.result.forEach((e) => {
            var format = e.pickingDate.split('T');
            var date = format[0];
            var time = format[1].split('.');
            this.hestoryNames.push({
              الاسم: e.pickedName,
              التاريخ: date,
              الوقت: time[0]
            });
          });
          this.isLoading = false;
        }).catch( () => {
          this.massage = 'حصل خطـأ';
          this.isLoading = false;
        });
    },
    timeDesply() {
      setTimeout(() => (this.isVisible = false), 4000);
    },
  },
  mounted() {
    this.timeDesply();
  },
};
</script>