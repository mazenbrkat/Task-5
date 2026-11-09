<h2>Search Task</h2>
<h4>-Reflection: </h4>
<p>feature that allows a program to inspect its own metadata at runtime,such as classes,methids,properties an fields.</p>
<p>فحص معلومات الكود بنفسه اثناء تشغيل  </p>
<p>Using System.Reflection;</p>
<h4>-Protected internal</h4>
<p>allows access from: </p>
<li>
  <ul>1. Any class within the same Assembly</ul>
  <ul>OR 2. Derived classes in another Assembly through inheritance</ul>
</li>
<h4>-Private Protected</h4>
<p>allows access only from derived classes with the same Assembly</p>

<h5>Protected Internal => Same Assembly OR Inheritance </h5>
<h5>Private Protected => Same Assembly AND Inheritance </h5>
