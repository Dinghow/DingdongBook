# DingdongBook

> **C“叮咚”— 在线书评及购书系统**

![](https://github.com/Dinghow/DingdongBook/raw/master/img/c2674995e13445f38fb61cfac48c500b.png)

# 1 系统需求

“叮咚”——在线书评及购书系统是一个可以为用户提供购书、评书的平台，用户可以在这个平台上查询各类图书的信息，查看他人对图书的评价并进行互动，同时可以对心意的图书进行添加收藏夹、购物车、购买等操作，系统的具体功能点及对应的需求见下。

1. **系统功能性需求**
   1. **用户系统需求**
      1. 用户账户系统
2. *用户注册*

> 创建一个新用户，初始化基本信息，包括用户名密码等。

1. *用户登录*

> 用户根据已注册过的账号密码进行登录。

1. *个人信息管理*

> 用户可修改基本信息，同时也包括收货地址的管理等。

1. 图书信息系统
2. *搜索图书*

> 用户可输入关键字进行搜索，或者按照图书的类别进行搜索，并展示搜索结果。

1. *查看图书具体信息*

> 用户可在图书信息页面查看图书的具体信息，包括简介、评价、评分等。

1. *查看个人收藏夹*

> 用户可查看已添加到收藏夹内的图书。

1. *管理个人收藏夹*

> 用户可对已添加到收藏夹内的图书进行移除。

1. *用户对图书进行评价*

> 完成订单后的用户可以对相应图书作出评分评价。

1. *查看他人对图书评价*

> 用户可在图书信息页面查看其它购买过此书的人对其的评价。

1. 图书购买系统
2. *加入购物车*

> 用户可在图书页面和首页将图书加入购物车。

1. *查看购物车信息*

> 用户可查看当前购物车信息，包括已加入购物车的图书及价格，总购物车的价格等。

1. *管理购物车信息*

> 用户可对已加入购物车内的图书进行移入收藏夹删除或者修改数量等操作。

1. *提交订单*

> 用户结算购物车后可以提交并保存此次购物的订单信息。

1. *查询订单信息*

> 用户可在个人中心查看已签收和未签收的订单信息。

1. *管理订单信息*

> 对于已完成的订单，用户可对其进行删除。

1. **管理员系统需求**
   1. 管理员登录

> 管理员可按照管理员账号与密码登录后台管理页面。

1. 搜索用户

> 管理员可以搜索指定用户，并展示结果。

1. 删除用户

> 管理员可以封禁指定用户，将其账号从数据库中删除。

1. 管理图书

> 管理员可以新增图书、删除现有图书或者修改现有图书的相关信息。

1. **网站系统需求**
   1. 新书上架

> 首页的“新书上架”可以动态展示图书上架时间最近的5本书。

1. 热门图书

> 首页的“热门推荐”可以动态展示用户评分最高的5本书。

1. 站长推荐

> 首页的“为您推荐”可以静态地展示由网站管理人员推荐的10本书。

1. **系统非功能性需求**
   1. **运行环境需求**

Windows、Mac OS，浏览器推荐Chrome或IE 8以上版本

1. **网络需求**

信息数据存储在服务器端的数据库中，各用户在规定的权限下在其客户端进行访问，同时实现信息共享。

1. **安全性需求**

后台数据不可随意进行更改，同时应具有保密性，不可泄露用户私人信息。

用户有规定的访问权限，不可进入管理员页面。

1. **鲁棒性需求**

对于用户录入的数据信息通过正则校验等方式进行限制，包括数据类型以及取值范围等，及时拦截不正确信息的传递。

1. **稳定性需求**

对数据库中的表添加主码约束、外码约束、一致性约束，并通过设置级联操作等来实现和保护数据库数据一致性、完整性。

1. **易用性需求**

界面整洁友好，用户操作方便，可快速上手访问所需信息。

# 2 个人模块功能设计与实现

**2.1 登录注册功能**

2.1.1登录注册功能设计

用户在注册时需要填写部分个人信息，包括个人用户名、密码及个人邮箱。再注册。系统在用户注册成功后会返回登录页面，然后用户可以根据自己的用户名和密码作为验证信息进行登录。未登录的用户可以查看首页信息，并进行搜索操作，但无法进行任何个人操作。

| **动作序列** | **描述**                                                     |
| ------------ | ------------------------------------------------------------ |
| 注册         | 用户通过输入用户名、密码、确认密码、个人邮箱信息后可以完成个人注册注册操作，以上输入信息为必填选项，并且需满足规范要求。 |
| 注册成功     | 注册成功后系统会返回登录页面允许用户进行登录操作或者取消登录并返回首页。 |
| 注册失败     | 注册失败后所有输入框的信息清空，不进行任何操作。             |
| 登录         | 用户通过输入用户名及密码可以进行登录操作，输入框信息为必填选项。 |
| 登录成功     | 系统在登录成功后会跳转至主页，用户状态为登录状态，可以进行全部用户操作。 |
| 登录失败     | 系统在在登录失败后会清除所有输入框的信息，并且不进行任何操作。 |
| 取消登录     | 用户在点击取消登录后，页面跳转至初始首页。                   |

2.1.2 登录注册功能实现

![](https://github.com/Dinghow/DingdongBook/raw/master/img/a596231e3ae7c21cf32825cc8e2831a4.png)

图2.1 用户登录

![](https://github.com/Dinghow/DingdongBook/raw/master/img/5db58fe11aab3c51a261d9fc129c600d.png)

图2.2 用户注册

![](https://github.com/Dinghow/DingdongBook/raw/master/img/fdbbbe0ccdea39c84ebc9b7c4ae0724e.png)

图2.3 顶部栏-登陆后

**2.2 修改个人信息**

2.2.1 修改个人信息功能设计

用户可在个人信息页面进行修改个人信息的操作，包括修改姓名、性别、个人邮箱、密码。

| **动作序列** | **描述**                                                     |
| ------------ | ------------------------------------------------------------ |
| 进入个人页面 | 用户在登录状态下可以通过页面上方的“个人中心”进入个人信息页面。 |
| 修改个人信息 | 此动作包括修改个人姓名、性别及邮箱，在用户对三项信息进行修改之后可以点击“确认修改”按键来保存修改的信息，三项信息为必填选项，否则无法修改。 |
| 修改密码     | 用户需输入旧密码及新密码来进行密码修改。                     |

2.2.2 修改个人信息功能实现

![](https://github.com/Dinghow/DingdongBook/raw/master/img/2b48234499fa5ed9e466ca2d18974b69.png)

图2.4 修改个人信息

**2.3 地址管理**

2.3.1 地址管理功能设计

用户可在个人中心页面进入地址管理页面，对自己的收货地址进行管理。

| **动作序列** | **描述**                                                     |
| ------------ | ------------------------------------------------------------ |
| 进入地址管理 | 用户可在登录状态下通过个人中心页面进入地址管理页面。         |
| 添加新地址   | 用户点击“添加地址”后可在弹出窗口内填写新地址信息，包括省、市、区、详细地址、姓名、邮编、电话号码。以上信息为必填选项。 |
| 删除地址     | 用户点击“管理”按键进入选择操作，之后可点击地址框进行选择或选择全选，选择完成后点击删除来删除地址。 |

2.3.2 地址管理功能实现

![](https://github.com/Dinghow/DingdongBook/raw/master/img/340d8a80fa137944993e1cc237d8aad4.png)

图2.5 管理个人地址

![](https://github.com/Dinghow/DingdongBook/raw/master/img/b47ab78e49f713824a9b0f3a4cd13925.png)

图2.6 添加新地址

# 3 图书模块功能设计与实现

**3.1 搜索图书**

3.1.1 搜索图书功能设计

用户可在搜索框内输入关键字或点击分类来进行搜索操作，结果在搜索页面展示，同时用户可点击图书进入相应的详情页面。

| **动作序列** | **描述**                                                     |
| ------------ | ------------------------------------------------------------ |
| 进入搜索     | 用户可在搜索框内输入关键词或点击分类来进行搜索，之后搜索结过跳转至搜索页面。 |
| 查看图书详情 | 用户可点击搜索结果中的图书来跳转至图书详情页面，查看图书详情及其他用户对图书的评论。 |

3.1.2 搜索图书功能实现

![](https://github.com/Dinghow/DingdongBook/raw/master/img/d82fea74c104f96dba1f459cf660a881.png)

图3.1 搜索图书

![](https://github.com/Dinghow/DingdongBook/raw/master/img/c56e8be5a922b813baa999548c54cab6.png)

图3.2 图书详情

**3.2 收藏夹**

3.2.1 收藏夹功能设计

用户可在图书详情页面及首页将心仪的图书加入收藏夹。用户可通过首页进入个人收藏夹，进行管理操作。

| **动作序列** | **描述**                                                     |
| ------------ | ------------------------------------------------------------ |
| 添加收藏     | 用户在图书详情页面点击“加入收藏夹”或者在首页相应的图书上方点击“收藏”可将图书加入收藏夹。 |
| 进入收藏夹   | 用户通过首页上方“我的叮咚”中的“我的收藏”可进入收藏夹页面。   |
| 切换查看模式 | 用户可点击收藏夹页面左上角的“缩略形式”和“详情形式”切换不同的查看模式。 |
| 删除藏书     | 用户点击“管理”按键进入选择操作，之后可点击图书框进行选择或选择全选，选择完成后点击删除来删除藏书。 |

3.2.2 收藏夹功能实现

![](https://github.com/Dinghow/DingdongBook/raw/master/img/64aa433663f80174b8df1b1284f5cab5.png)

图3.3 收藏夹-缩略

![](https://github.com/Dinghow/DingdongBook/raw/master/img/94c408da43e41c4a768264b8209c4ead.png)

图3.4 收藏夹-详细

![](https://github.com/Dinghow/DingdongBook/raw/master/img/f193b5083cda1ddadbde0e8bd9c38898.png)

图3.5 管理收藏夹

**3.3 图书评论**

3.3.1 图书评论功能设计

用户在订单完成后，在订单签收页面可对购买后的图书进行评价并评分。此外用户可在详情页面查看图书评价。

| **动作序列** | **描述**                                                     |
| ------------ | ------------------------------------------------------------ |
| 进入订单     | 用户可通过首页“我的叮咚”进入订单页面，切换到“已签收”可对图书进行评价。 |
| 评价图书     | 用户可在已签收图书订单中点击“评价”，并在弹出窗口中点击星星进行评分，在输入框中输入对该书的评价，为必填选项，击“提交”提交评论。 |

3.3.2 图书评论功能实现

![](https://github.com/Dinghow/DingdongBook/raw/master/img/bc7af2120ca3e5eee9555923ff6c481b.png)

图3.6 图书评论

# 4购物模块功能设计与实现

**4.1 购物车**

4.1.1 购物车功能设计

用户可在图书详情页面及首页将心仪的图书加入购物车。用户可通过首页进入“我的购物车”，进行管理操作。

| **动作序列** | **描述**                                                     |
| ------------ | ------------------------------------------------------------ |
| 添加购物车   | 用户在图书详情页面点击“加入购物车”或者在首页相应的图书上方点击“加入购物”可将图书加入个人购物车。 |
| 进入购物车   | 用户通过首页“购物车”可进入个人购物车页面。                   |
| 修改数量     | 用户可在相应图书的数量一栏点击加号或减号对购物车图书的数量进行修改，购物车总金额也会随之自动修改。 |
| 移入收藏夹   | 用户可点击相应图书后的“移入收藏夹”来将该书放入收藏夹中。     |
| 删除         | 用户可点击相应图书后的“删除”或者自行勾选图书来批量删除购物车的内容。 |
| 结算         | 用户点击“结算”按钮跳转至订单模块进行结算。                   |

4.1.2 购物车功能实现

![](https://github.com/Dinghow/DingdongBook/raw/master/img/062a433fb1bebc38b4542a2b3cf50daf.png)

图4.1 购物车管理

**4.2 订单**

4.2.1 订单功能设计

用户在结算完购物车后购物车后进入订单页面，在此页面确认相关信息，包括送货地址的选择以及订单内容的确认。

| **动作序列** | **描述**                                                     |
| ------------ | ------------------------------------------------------------ |
| 进入订单     | 用户在确认购物车结算内容后可点击“结算”进入订单页面。         |
| 地址选择     | 用户可以点击已有的个人地址的地址框进行此次订单的的收货地址选择，或者点击“使用新地址”，在弹出框中填入此订单收货地址的信息。 |
| 返回购物车   | 用户可点击页面下方的“返回购物车”，终止交易。                 |
| 提交订单     | 用户可点击“提交订单”按键提交订单，跳转至反馈页面。           |

4.2.2 订单功能实现

![](https://github.com/Dinghow/DingdongBook/raw/master/img/e48c0e8b224e61e6f034b4a41eb8dc01.png)

图4.2 订单管理-未签收

![](https://github.com/Dinghow/DingdongBook/raw/master/img/6099f81d8b83715afbfcdd2c3b112f02.png)

图4.3 订单管理-已签收

![](https://github.com/Dinghow/DingdongBook/raw/master/img/c07a5fb0fb5bd31b03b3c9d8f11dd866.png)

图4.4 确认订单

![](https://github.com/Dinghow/DingdongBook/raw/master/img/a3bae222721d24593ad27ca5f2c749ce.png)

图4.5 确认订单时使用新地址

**4.3 订单管理**

4.3.1 订单管理功能设计

用户确认订单后可以通过用户的“我的订单”进入订单管理页面，在此页面确认相关信息，包括订单号，书目种类，书本数量，书本简介等。

| **动作序列** | **描述**                                                     |
| ------------ | ------------------------------------------------------------ |
| 进入订单管理 | 用户在主页面后可点击“我的订单”进入订单管理页面。             |
| 查看订单信息 | 用户可以确认订单的详细信息，包括订单号、包含的书目种类、书本数量、订单总价、书本简介等。 |
| 确认收货     | 用户可点击“确认收货”按键，将未收货的订单改为已收货的订单。   |
| 删除订单     | 用户可点击“删除订单”按键删除该用户欲删除的已签收订单。       |
| 评论图书     | 用户可点击“评论”按键，对订单中的单一种类的书进行评价。       |

4.3.2 订单管理功能实现

如图，用户在主页面点击“我的订单”进入订单管理页面。

如图，用户可以在我的订单页面看见该用户的订单详情。

如图，用户可以将未收货的订单改为已签收的订单。

如图，用户可以删除欲删除的已签收订单。

如图，用户可以对已签收订单的其中一本书进行评价。

# 5管理模块功能设计与实现

**5.1 登录**

5.1.1 登录功能设计

管理员可通过页面最下方的管理员登录接口，进入管理员登录模式，登录成功后可进行全部的管理员操作。

| **动作序列** | **描述**                                                     |
| ------------ | ------------------------------------------------------------ |
| 进入登录界面 | 管理员在点击页面最下方的“管理员登录”后可跳转至登录界面。     |
| 登录         | 管理员通过输入用户名及密码可以进行登录操作，输入框信息为必填选项。 |
| 登录成功     | 系统在登录成功后会出现管理员操作页面，可以进行全部管理员操作。 |
| 登录失败     | 系统在在登录失败后会清除所有输入框的信息，并且不进行任何操作。 |
| 取消登录     | 管理员在点击取消登录后，页面跳转至初始首页。                 |

5.1.2 登录功能实现

![](https://github.com/Dinghow/DingdongBook/raw/master/img/14c9f36f4184091aa3ab9416d64ac03a.png)

图5.1 首页管理员入口

![](https://github.com/Dinghow/DingdongBook/raw/master/img/3c5aa1ddbe2b2e7d7f392250f1738562.png)

图5.2 管理员登陆

**5.2 管理员操作**

5.2.1 管理员操作功能设计

管理员在登录成功后，可以在管理员页面进行管理员操作，包括用户管理和图书管理。

| **动作序列**   | **描述**                                                     |
| -------------- | ------------------------------------------------------------ |
| 进入管理员页面 | 管理员在登录成功后会跳转至管理员页面，                       |
| 用户管理       | 管理员在点击“用户管理”后可输入关键词搜索用户，搜索得到的用户会展示在搜索框下方。管理员可点击删除来封禁对应的用户。 |
| 添加图书       | 管理员在点击“添加图书”后可通过输入相应信息来添加图书，包括书籍名称、ISBN、作者、类别、出版社、出版时间、价格、封面和简介，以上信息为必填选项。 |
| 修改图书信息   | 管理员在点击“修改图书信息”后可输入关键词搜索图书，搜索得到的图书会展示在搜索框下方。管理员可点击“删除图书”来下架相应的图书。此外可点击“修改图书信息”，在弹窗中来修改相应图书的信息。 |

5.2.2 管理员操作功能实现

![](https://github.com/Dinghow/DingdongBook/raw/master/img/6d4f07c70b134124a954f6cf66542d25.png)

图5.3 管理员搜索并管理用户

![](https://github.com/Dinghow/DingdongBook/raw/master/img/f6a8428b2d615a7044bf8621b86840d1.png)

图5.4 管理员搜索并修改图书信息

![](https://github.com/Dinghow/DingdongBook/raw/master/img/a6d0103b338f8350e9038557f0cc3bb0.png)

图5.5 管理员添加图书

# 6关系模式

**6.1 数据库关系图**

![未命名表单 (https://github.com/Dinghow/DingdongBook/raw/master/img/9316cb8c150ebbc86edb5d976836f723.png)](media/9316cb8c150ebbc86edb5d976836f723.png)

**6.2 总体E-R图**

![未命名表单 (https://github.com/Dinghow/DingdongBook/raw/master/img/9126e4bbc91bcfbcfb4b41a02e575fe0.png)](media/9126e4bbc91bcfbcfb4b41a02e575fe0.png)

**6.3 用户模块E-R图**

![用户](https://github.com/Dinghow/DingdongBook/raw/master/img/3477b0f0e962006661e1e04e0db6bc27.png)

**6.4 书籍模块E-R图**

![书籍 (https://github.com/Dinghow/DingdongBook/raw/master/img/c0c316f811a1c6a41b0fa662f55e430d.png)](media/c0c316f811a1c6a41b0fa662f55e430d.png)

**6.5 订单模块E-R图**

![订单 (https://github.com/Dinghow/DingdongBook/raw/master/img/f4d82e62732b02d8f81c4fb68a76fdc3.png)](media/f4d82e62732b02d8f81c4fb68a76fdc3.png)

**6.6 评论模块E-R图**

![评论](https://github.com/Dinghow/DingdongBook/raw/master/img/7d3c7affebf49c809cbe7a47783b49e4.png)

# 7数据库表

**7.1 user表**

表格 7-1 user表

| 字段名    | 数据类型 | 长度 | 说明     | 备注                        |
| --------- | -------- | ---- | -------- | --------------------------- |
| ID        | number   |      | 用户ID   | PK                          |
| name      | varchar  | 20   | 用户姓名 |                             |
| authority | varchar  | 20   | 权限     |                             |
| account   | varchar  | 20   | 帐号     |                             |
| password  | varchar  | 20   | 密码     | 表中存储的是实际密码的md5值 |
| gender    | varchar  | 10   | 性别     |                             |
| Email     | varchar  | 40   | 邮箱     |                             |

**7.2 address表**

表格 7-2 address表

| 字段名    | 数据类型 | 长度 | 说明       | 备注 |
| --------- | -------- | ---- | ---------- | ---- |
| ID        | number   | 38   | 地址ID     | PK   |
| name      | varchar  | 20   | 收货人姓名 |      |
| phone     | varchar  | 20   | 电话       |      |
| country   | varchar  | 20   | 国家       |      |
| province  | varchar  | 20   | 省份       |      |
| city      | varchar  | 20   | 城市       |      |
| district  | varchar  | 20   | 区         |      |
| post_code | number   | 10   | 邮编       |      |
| user_id   | number   |      | 用户ID     |      |
| location  | varchar  | 100  | 具体地址   |      |

**7.3 live表**

表格 7-3 live表

| 字段名     | 数据类型 | 长度 | 说明   | 备注                   |
| ---------- | -------- | ---- | ------ | ---------------------- |
| user_ID    | number   |      | 用户ID | PK,FK，参照user的ID    |
| address_ID | number   | 38   | 地址ID | PK,FK，参照address的ID |

**7.4 book表**

表格 7-4 book表

| 字段名       | 数据类型 | 长度 | 说明     | 备注 |
| ------------ | -------- | ---- | -------- | ---- |
| ID           | number   | 38   | 图书ID   | PK   |
| ISBN         | varchar  | 20   | 图书ISBN |      |
| name         | varchar  | 30   | 图书名   |      |
| price        | number   |      | 价格     |      |
| image        | varchar  | 40   | 图片路径 |      |
| category     | varchar  | 20   | 图书分类 |      |
| publisher    | varchar  | 100  | 出版社   |      |
| Publish_time | varchar  | 20   | 出版时间 |      |
| abstract     | varchar  | 400  | 摘要     |      |

**7.5 author表**

表格 7-5 author表

| 字段名 | 数据类型 | 长度 | 说明   | 备注 |
| ------ | -------- | ---- | ------ | ---- |
| ID     | number   |      | 作者ID | PK   |
| name   | varchar  | 20   | 姓名   |      |

**7.6 write表**

表格 7-6 write表

| 字段名    | 数据类型 | 长度 | 说明   | 备注                  |
| --------- | -------- | ---- | ------ | --------------------- |
| author_ID | number   |      | 作者ID | PK,FK，参照author的ID |
| book_ID   | number   |      | 图书ID | PK,FK，参照book的ID   |

**7.7 category表**

表格 7-7 category表

| 字段名 | 数据类型 | 长度 | 说明   | 备注 |
| ------ | -------- | ---- | ------ | ---- |
| ID     | number   |      | 分类ID | PK   |
| name   | varchar  | 20   | 名称   |      |

**7.8 belong表**

表格 7-8 belong表

| 字段名      | 数据类型 | 长度 | 说明   | 备注                    |
| ----------- | -------- | ---- | ------ | ----------------------- |
| book_ID     | number   |      | 图书ID | PK,FK，参照book的ID     |
| category_ID | number   |      | 分类ID | PK.FK，参照category的ID |

**7.9 orders表**

表格 7-9 orders表

| 字段名     | 数据类型 | 长度 | 说明     | 备注                |
| ---------- | -------- | ---- | -------- | ------------------- |
| ID         | number   |      | 订单ID   | PK                  |
| user_ID    | number   |      | 购买人ID | FK，参照user的ID    |
| address_ID | number   |      | 地址ID   | FK，参照address的ID |
| quantity   | number   |      | 图书总数 |                     |
| price      | number   |      | 图书总价 |                     |
| remark     | varchar  | 100  | 备注     |                     |
| time_start | varchar  | 20   | 下单时间 |                     |
| time_get   | varchar  | 20   | 收货时间 |                     |
| status     | varchar  | 20   | 订单状态 | 到货或者未到货      |
| post_cost  | number   |      | 快递费用 |                     |

**7.10 order_include表**

表格 7-10 order_include表

| 字段名   | 数据类型 | 长度 | 说明         | 备注                |
| -------- | -------- | ---- | ------------ | ------------------- |
| order_ID | number   |      | 订单ID       | PK,FK,参照order的ID |
| book_ID  | number   |      | 图书ID       | PK,FK,参照book的ID  |
| quantity | number   |      | 该本图书数量 |                     |
| price    | number   |      | 该本图书总价 |                     |

**7.11 favorite表**

表格 7-11 favorite表

| 字段名  | 数据类型 | 长度 | 说明   | 备注               |
| ------- | -------- | ---- | ------ | ------------------ |
| user_ID | number   |      | 用户ID | PK,FK,参照user的ID |
| book_ID | number   |      | 图书ID | PK,FK,参照book的ID |

**7.12 cart表**

表格 7-12 cart表

| 字段名      | 数据类型 | 长度 | 说明               | 备注               |
| ----------- | -------- | ---- | ------------------ | ------------------ |
| user_ID     | number   |      | 用户ID             | PK,FK,参照user的ID |
| quantity    | number   |      | 购物车中图书总数量 |                    |
| time_start  | varchar  | 20   | 添加至购物车的时间 |                    |
| post_cost   | number   |      | 快递费用           |                    |
| total_price | number   |      | 总费用             |                    |

**7.13 cart_include表**

表格 7-13 cart_include表

| 字段名      | 数据类型 | 长度 | 说明             | 备注               |
| ----------- | -------- | ---- | ---------------- | ------------------ |
| user_ID     | number   |      | 用户ID           | PK,FK,参照user的ID |
| book_ID     | number   |      | 图书ID           | PK.FK,参照book的ID |
| quantity    | number   |      | 该本图书总数量   |                    |
| total_price | number   |      | 该本图书的总价格 |                    |

**7.14 comments表**

表格 7-14 comments表

| 字段名        | 数据类型 | 长度 | 说明                   | 备注               |
| ------------- | -------- | ---- | ---------------------- | ------------------ |
| ID            | number   |      | 评论ID                 | PK                 |
| user_ID       | number   |      | 用户ID                 | FK，参照user的ID   |
| book_ID       | number   |      | 图书ID                 | FK，参照了book的ID |
| title         | varchar  | 100  | 评论标题               |                    |
| content       | varchar  | 2000 | 评论内容               |                    |
| time          | varchar  | 20   | 评论时间               |                    |
| score         | number   |      | 评分                   |                    |
| total_like    | varchar  | 20   | 总赞同数               |                    |
| total_dislike | varchar  | 20   | 总反对数               |                    |
| total         | number   | 38   | 总赞同数与总反对数之差 |                    |

**7.15 comment_feedback表**

表格 7-15 comment_feedback表

| 字段名     | 数据类型 | 长度 | 说明             | 备注                    |
| ---------- | -------- | ---- | ---------------- | ----------------------- |
| user_ID    | number   |      | 用户ID           | PK,FK,参照了user的ID    |
| comment_ID | number   |      | 评论ID           | PK,FK,参照了comment的ID |
| attitude   | varchar  | 20   | 对于该评价的态度 |                         |
| time       | varchar  | 20   | 评价时间         |                         |

# 8附录A 图表索引

# 9附录B 重要类和函数

## **9.1 Entities类**

该类代表EF结构中的实体类，对应着服务器上的数据库，其中每一个DBSet\<\>类型的成员变量对应着数据库中的关系实例，可以对其进行get和set的操作。

成员变量：

```C#
    public virtual DbSet<ADDRESS> ADDRESS { get; set; } 
    public virtual DbSet<AUTHOR> AUTHOR { get; set; } 
    public virtual DbSet<BOOK> BOOK { get; set; } 
    public virtual DbSet<CART> CART { get; set; } 
    public virtual DbSet<CART_INCLUDE> CART_INCLUDE { get; set; } 
    public virtual DbSet<CATEGORY> CATEGORY { get; set; } 
    public virtual DbSet<COMMENT_FEEDBACK> COMMENT_FEEDBACK { get; set; } 
    public virtual DbSet<COMMENTS> COMMENTS { get; set; } 
    public virtual DbSet<ORDER_INCLUDE> ORDER_INCLUDE { get; set; } 
    public virtual DbSet<ORDERS> ORDERS { get; set; } 
    public virtual DbSet<USERS> USERS { get; set; } 
    public virtual DbSet<WRITE> WRITE { get; set; } 
    public virtual DbSet<CARTLIST> CARTLIST { get; set; } 
    public virtual DbSet<PURCHASE> PURCHASE { get; set; } 
    public virtual DbSet<ZUOZHE> ZUOZHE { get; set; }
```

## **9.2 Models类**

这种类型的类对应数据库中的关系模式，其成员变量为关系模式中的所有属性和其get和set方法，以及其主码被其他关系引用外码约束的记录和相应的get与set方法。

其中几个较为重要的类的成员变量如下：

##### 9.2.1 BOOK类：

```C#
    public decimal ID { get; set; }
    public string ISBN { get; set; }
    public string NAME { get; set; }
    public Nullable<decimal> PRICE { get; set; }
    public string IMAGE { get; set; }
    public string CATEGORY { get; set; }
    public string PUBLISHER { get; set; }
    public string PUBLISHTIME { get; set; }
    public string ABSTRACT { get; set; }//BOOK关系的属性

   [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<CART_INCLUDE> CART_INCLUDE { get; set; }//其主码被CART_INCLUDE所引用为外码约束，下面类似
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<COMMENTS> COMMENTS { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<ORDER_INCLUDE> ORDER_INCLUDE { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<WRITE> WRITE { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<CATEGORY> CATEGORY1 { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<USERS> USERS { get; set; }
```

##### 9.2.2 USERS类：

```C#
    public decimal ID { get; set; }、
    public string NAME { get; set; }
    public string GENDER { get; set; }
    public string EMAIL { get; set; }
    public string PASSWORD { get; set; }
    public string ACCOUNT { get; set; }
    public string AUTHORITY { get; set; }//USERS关系的属性    

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<ADDRESS> ADDRESS { get; set; }//其主码被ADDRESS引用，下面类似
    public virtual CART CART { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<CART_INCLUDE> CART_INCLUDE { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<COMMENT_FEEDBACK> COMMENT_FEEDBACK { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<COMMENTS> COMMENTS { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<ORDERS> ORDERS { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<BOOK> BOOK { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<ADDRESS> ADDRESS1 { get; set; }
```

##### 9.2.3 CART类：

```c#
   public decimal USER_ID { get; set; }
   public Nullable<decimal> QUANTITY { get; set; }
   public string TIME_START { get; set; }
   public Nullable<decimal> POST_COST { get; set; }
   public Nullable<decimal> TOTAL_PRICE { get; set; }//CART关系的属性
```

##### 9.2.4 CART_INCLUDE类：

```C#
    public decimal USER_ID { get; set; }
    public decimal BOOK_ID { get; set; }
    public Nullable<decimal> QUANTITY { get; set; }
    public Nullable<decimal> TOTAL_PRICE { get; set; }//CART_INCLUDED关系的属性
```

## **9.3 视图类**

为了进行查询的方便（例如查询购物车和订单中图书的详细信息，对应作者与图书），我们在数据库中建立了一些视图，这些视图也在Models中有相对应的类，如下：

##### 9.3.1 CARTLIST类：

根据CART_INCLUDE关系和BOOK关系生成，可以知道购物车中图书的信息和购买情况：

```C#
    public decimal USER_ID { get; set; }
    public decimal BOOK_ID { get; set; }
    public string NAME { get; set; }
    public Nullable<decimal> PRICE { get; set; }
    public string IMAGE { get; set; }
    public Nullable<decimal> QUANTITY { get; set; }
    public Nullable<decimal> TOTAL_PRICE { get; set; }
```

##### 9.3.2 ZUOZHE类：

根据AUTHOR关系，WRTIE关系和BOOK关系生成，将图书信息和作者信息根据写联系对应起来：

```C#
	public string AUTHOR_NAME { get; set; }
	public decimal BOOK_ID { get; set; }
	public decimal AUTHOR_ID { get; set; }
```

##### 9.3.3 PURCHASE类：

根据ORDER_INCLUDE关系，BOOK关系生成，可以知道订单中图书的信息和购买情况：

```C#
    public decimal ORDER_ID { get; set; }
    public string NAME { get; set; }
    public Nullable<decimal> PRICE { get; set; }
    public string IMAGE { get; set; }
    public Nullable<decimal> QUANTITY { get; set; }
    public Nullable<decimal> TOTAL_PRICE { get; set; }
    public string ABSTRACT { get; set; }
    public string PUBLISHER { get; set; }
    public string AUTHOR_NAME { get; set; }
    public decimal ID { get; set; }
    public decimal AUTHOR_ID { get; set; }
```

## **9.4 Controllers类**

Controllers类为控制器中的类，用于返回视图展示页面，响应页面上进行的操作请求，其中重点问题是要完成控制器和视图之间的数据传递。从控制器传递至视图主要采用Razor引擎和ViewBag；从视图传递至控制器使用了HtmlHelpers、Session和AJAX。

##### 9.4.1 BOOKsController类：

主要包含图书详情页面的展示和响应对图书进行加入购物车请求的方法，成员函数如下：

```C#
	public ActionResult Index(int ID)//读取图书详情页面所需的所有数据，并返回视图进行展示，参数为图书ID
	public ActionResult addCart(cartSender cs)//将该图书添加至用户购物车中，通过重定位刷新原页面，如果没有用户登录则不进行操作并跳转至登录页面，参数为利用HTTPPOST从视图传递数据的model
	public int directAddCart(int bookId)//在首页将点击的图书添加至用户购物车，成功返回0，无用户登录则返回-1，参数为图书ID
```

##### 9.4.2 FavoriteController类：

主要包含收藏夹页面的展示和响应改变收藏夹请求的方法，该控制器的所有操作的成功执行都是需要在有用户登录的情况下，成员函数如下：

```C#
	public ActionResult Index(int ID)//读取收藏夹页面所需的所有数据，并返回视图进行展示，在没有用户登录的情况下不进行操作并跳转至登录页面，参数为用户ID
	public ActionResult addFav(int ID)//在图书详情页面将图书加入收藏夹，通过重定位刷新原页面，如果没有用户登录则不进行操作并跳转至登录页面，参数为图书ID
	public int directAddFav(int bookId)//在首页将点击的图书添加至收藏夹，成功返回0，无用户登录则返回-1，参数为图书ID
	public void DeleteFav(int[] bookIds)//将所有选中的图书移除收藏夹，参数为选中的图书的ID
```

##### 9.4.3 CARTsController类：

主要包含购物车页面的展示和对其页面上所有操作请求响应的方法，该控制器的所有操作的成功执行都是需要在有用户登录的情况下，成员函数如下：

```C#
	public ActionResult Index(int ID)//读取购物车页面所需的所有数据，并返回视图进行展示，在没有用户登录的情况下不进行操作并跳转至登录页面，参数为用户ID
	public void RemoveBook(int bookId)//将购物车中的某个图书项目移出购物车，参数为图书ID
	public void AddFav(int bookId)//将购物车中的某个图书移入收藏并移出购物车，参数为图书ID
	public void EditAmount(int bookId,int bookAmount)//通过购物车页面的加减按钮改变购物车中某一图书的数量，参数为用户ID和改变后的数量
	public ActionResult AddAddress(ADDRESS aDDRESS)//响应购物车页面上方地址栏的添加新地址请求，参数为通过HTTPPOST传递的参数model
```

##### 9.4.4 OrdersController类：

主要包含订单管理页面与订单填写和完成页面的展示以及对这些页面上所有操作请求响应的方法，该控制器的所有操作都是需要在用户登录的情况下，成员函数如下：

```C#
	public ActionResult Index(int ID)//读取订单管理页面所需的所有数据，并返回视图进行展示，在没有用户登录的情况下不进行操作并跳转至登录页面，参数为用户ID	
	public void StatusChange(int orderId)//在订单管理页面确认收货，参数为订单ID
	public void DeleteOrder(int orderId)//删除已经签收的订单，参数为订单ID
	public void SetComment(int bookId,int grade,string content)//对已经签收的图书进行评论，参数为图书ID，分数和评论内容
	public int GetOrder(int[] bookIds)//将购物车中勾选的书目添加至session，返回用户ID
	public ActionResult Process_CART(int ID)//读取购物车结算时的购买情况，据此返回一个显示订单填写页面，参数为用户ID
	public void check(int addrId)//根据选择的地址和购买情况生成订单，参数为地址ID
	public ActionResult Order_Complete()//显示订单填写成功的页面
```

##### 9.4.5 AdminController类：

主要包含管理员页面的展示和响应页面上所有操作请求的方法，该控制器的所有操作都是需要在管理员登录的情况下，主要成员函数如下：

```c#
	 public ActionResult Index(string search_book,string search_user)//返回管理员操作页面，参数为搜索的图书或用户的字符串
	 public ActionResult Login()//返回管理员登录页面
	 public ActionResult Login(LoginSender msg)//管理员登录的验证，参数为利用HTTPPOST传递参数的model
	 public void deleteUser(int ID)//删除用户，参数为用户ID
	 public void deleteBook(int ID)//删除书籍，参数为图书ID
	 public void adjustBook(int ID, string name, string ISBN, string writer, string category, string publisher, string time, string image, string intro)//修改书籍
```