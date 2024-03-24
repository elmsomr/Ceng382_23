document.addEventListener('DOMContentLoaded', (event) => {
    const toggleButton = document.querySelector('.btn-secondary');
    toggleButton.addEventListener('click', function(e) {
      e.preventDefault();
      const elementsToToggle = document.querySelectorAll('.mb-4, .fw-normal, .form-floating, .btn-primary, .text-body-secondary');
      elementsToToggle.forEach(function(element) {
        if (element.style.display === 'none') {
          element.style.display = 'block';
        } else {
          element.style.display = 'none';
        }
      });
    });
  });

  document.addEventListener('DOMContentLoaded', (event) => {
    const calculateButton = document.querySelector('.btn-primary');
    calculateButton.addEventListener('click', function(e) {
      e.preventDefault();
      const firstNumber = parseFloat(document.getElementById('firstNumber').value);
      const secondNumber = parseFloat(document.getElementById('secondNumber').value);
      const sum = firstNumber + secondNumber;
      const resultParagraph = document.getElementById('resultParagraph');
      resultParagraph.style.display = 'block';
      resultParagraph.textContent = 'The sum is: ' + sum;
    });
  });
  
  