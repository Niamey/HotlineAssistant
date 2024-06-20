export default {
  units: {
    pieces: 'шт',
    currensy: {
      uah: 'UAH',
      usd: 'USD'
    }
  },
  components: {
    authorization: {
      login: {
        title: 'Увійдіть до свого акаунта',
        login: 'Введіть логін',
        password: 'Пароль',
        message: 'Даний акаунт недоступний. Зверніться за детальною інформацією <a href="mailto:help@bankvostok.com.ua" class="text-primary">help@bankvostok.com.ua</a>'
      },
      logout: {
        sessionPaused: 'Сесія припинена',
        youWillBeLogout: 'Ви будите відключені через',
        clickContinueForStayInSystem: 'Натисніть продовжити, щоб залишатися в системі.',
        continue: 'Продовжити',
        sessionClosed: 'Сесія була завершена по закінченню часу'
      },
      update: {
        newVersionAvailable: 'Нова версія доступна',
        newVersionInfoText: 'Нова версія програми була розгорнута з моменту останнього використання системи. Сторінка буде оновлена.',
        continue: 'Продовжити'
      }
    },
    userProfile: {
      status: 'Статус',
      sendFeedback: 'Надіслати відгук',
      profile: 'Профіль',
      logout: 'Вийти'
    },
    feedback: {
      title: 'Залишити відгук',
      comment: 'Коментар чи побажання',
      rateInterface: 'Оцініть зручність інтерфейсу',
      send: 'Надіслати',
      status: {
        terribly: 'Жахливо',
        badly: 'Погано',
        normally: 'Нормально',
        fine: 'Добре',
        perfectly: 'Чудово'
      }
    },
    client: {
      clientSearch: {
        searchBy: 'Шукати за'
      },
      clientSearchCategory: {
        label: 'Категорія'
      },
      clientSearchAdvance: {
        title: 'Розширений пошук',
        btnSearchLabel: 'Пошук',
        options: {
          hint: 'Тип пошуку',
          inn: 'ІПН',
          initials: 'ПІБ',
          iban: 'IBAN',
          cardNumber: 'Номер картки',
          phone: 'Номер телефону',
          passport: 'Паспорт',
          barcode: 'Штрих-код картки',
          energo: 'Операція "Енерго"'
        },
        labels: {
          inn: 'ІПН',
          initials: 'Прізвище, Ім\'я, По батькові',
          iban: 'IBAN',
          cardNumber: 'Номер картки',
          phone: 'Номер мобільного телефону',
          passport: 'Паспорт',
          barcode: 'Штрих-код картки',
          energo: 'Операція "Енерго"'
        }
      },
      clientSearchResult: {
        columns: {
          fullName: 'ПІБ',
          status: 'Статус',
          birthDate: 'Дата народження',
          inn: 'ІПН'
        },
        noCounterpart: 'відсутня інформація за критеріями пошуку в базі Solar'
      },
      detail: {
        clientRegistrationData: {
          label: 'Реєстраційні дані клієнта',
          inn: 'ІПН:',
          passportData: 'Паспортні дані',
          documentType: 'Тип документу',
          documentSeriesAndNumber: 'Серія і номер',
          documentIssuedDate: 'Дата видачі',
          citizenship: 'Громадянство',
          documentIssuer: 'Ким виданий',
          documentExpirationDate: 'Дійсний до',
          documentPhotoDate: 'Дата фото 25/45 років',
          birthPlace: 'Місце народження',
          birthDate: 'Дата народження',
          citizenshipCountry: 'Громадянство',
          residency: 'Резидентність',
          addressOfResidence: 'Адреса місця проживання',
          registrationAddress: 'Адреса місця реєстрації',
          email: 'Email',
          job: 'Компанія',
          position: 'Посада',
          password: 'Кодове слово',
          gender: {
            label: 'Стать',
            male: 'Чоловіча',
            female: 'Жіноча'
          },
          labelExpand: 'Переглянути всі дані клієнта',
          labelCollapse: 'Закрити всі дані клієнта'
        },
        agreements: {
          clientAgreementList: {
            title: 'Договори',
            toggle: {
              showDebt: 'Сума заборгованості',
              showInactive: 'Неактивні'
            },
            columns: {
              id: '#',
              number: '№',
              project: 'Проект',
              status: 'Статус договору',
              debt: 'Заборгованість'
            },
            totalDebt: 'Весь борг',
            history: {
              title: 'Історія. Статус договору'
            }
          },
          clientAgreementDetail: {
            label: 'Інформація за договором',
            client: 'Клієнт',
            status: {
              label: 'Статус',
              active: 'Активний'
            },
            type: 'Вид',
            project: 'Проект',
            bonusProgram: 'Бонусна програма',
            locksLabel: 'Обмеження за договором',
            noLocks: 'Обмежень немає',
            debt: 'Заборгованість',
            missing: 'Відсутня'
          },
          clientAgreementTabs: {
            title: 'Договір',
            detail: 'Деталі договору',
            statement: 'Виписка',
            tariffs: 'Тариф',
            debt: 'Заборгованість',
            moneyBox: 'Назбиратус'
          },
          ClientExtractFromContract: {
            label: 'Виписка',
            isActive: 'Відключити'
          },
          ClientAgreementTariff: {
            label: 'Тариф'
          },
          moneyBoxList: {
            name: 'Назва',
            status: 'Статус',
            balance: 'Баланс'
          },
          audit: {
            agreementStatusHistory: {
              title: 'Історія. Статус договору',
              commentsTitle: 'Коментарі',
              status: 'Поточний статус',
              futureStatus: 'Майбутній статус',
              dateOfConclusion: 'Дата та час укладання',
              dateOfChange: 'Дата та час зміни',
              comment: 'Причина зміни статусу'
            }
          }
        },
        account: {
          clientAccountList: {
            title: 'Рахунки',
            columns: {
              id: '#',
              accountNumber: '№ рахунку',
              currency: 'Валюта',
              accountType: 'Тип',
              status: 'Статус'
            },
            accountType: {
              // main: 'Основний',
              // additional: 'Додатковий',

              device1004: '1004 касса',
              int2608: '2608 % - Проценты на остаток основного СКС ЮЛ',
              transit2920: '2920 транзит АТМ',
              dispute2924: '2924 диспут',
              shortage2924: '2924 недостачи',
              surplus2924: '2924 излишки',
              transit2924: '2924 транзит',
              fee3570: '3570 срочная',
              feeOvd3570: '3570 просрочка',
              fee3578: '3578 срочная',
              feeOvd3578: '3578 просрочка',
              cashback3678: '3678 cash-back',
              limit9129: '9129 Обязательства',
              damping96019618: '9601/9618 гашение за счет резерва РКО',
              reserve9601: '9601 резерв',
              acc2924AdviceOffus: 'ACC_2924 Псевдо-онлайн off-us (13)',
              acc3622Ndfl: '3622 НДФЛ',
              acc6050: '6050 %% доходи по кредитам',
              acc6052: '6052 %% доходи по кредитам прострочені',
              acc2924P2PVb: 'ACC_2924_P2P (57)',
              acc2924Transit29: 'ACC_2924 - Транзит (для переводов Solar-Solar - 29)',
              acc3622MilitaryFee: '3622 Воен сб -1,5% налог на %%',
              acc3800: '3800 Валютна позиція (транзакції)',
              acc3801Eur: '3801 Эквівалент ВП (транзакції) (980-978)',
              acc3801Usd: '3801 Эквівалент ВП (транзакції) (980-840)',
              acc6397: '6397 Пеня',
              acc6510: '6510 Комісія за РКО, транзакційна',
              acc6510Cash: '6510 Комісія за касове обслуговування',
              acc6514: '6514 Комісія за ковертацію',
              acc7020: '7020 % на остатки на карт.сч.СХД в нац.валюте',
              acc7020Val: '7020 % на остатки на карт.сч.СХД в ин.валюте',
              acc7040: '7040 % на остатки на карт. сч ФЛ (2620 - НВ)',
              acc7040Val: '7040 % на остатки на карт. сч ФЛ (2620 - СКВ)',
              acc9900: '9900 контррсчет',
              acc9910: '9910 контррсчет',
              over: '2063/2620 с/о',
              overExpire2809Bpk: '2809 просроч фин/дебиторка',
              overInt: '2607/2627 % с/о',
              overIntOvd: '2068/2208 просроч % с/о',
              overOvd: '2063/2203 просроч с/о',
              overOvdInt: '2068/2208 % на просроч с/о',
              overOvdIntOvd: '2068/2208 просроч % начисл на просроч часть с/о',
              overRest2809Bpk: '2809 фин/дебиторка',
              sks: '2600/2620/2904',
              sksInt: '2628 % - Проценты на остаток основного СКС',
              асс2924P2P: 'ACC_2924_P2P (67) не наш WB'
            }
          }
        },
        cards: {
          clientCardTabs: {
            title: 'Картка',
            detail: 'Дані картки',
            stopList: 'Стоп-лист',
            limitation: 'Лiмити',
            tokens: 'Токени',
            services: 'Сервіси',
            balance: 'Баланс',
            balances: 'Баланси',
            replenishment: 'Поповнення',
            trip: 'Поїздка',
            debt: 'Заборгованість'
          },
          cardFullBalance: {
            available: 'Доступний залишок',
            own: 'Власні кошти',
            blocked: 'Заблоковано',
            // delayed: 'Відкладено'; (todo check this later)
            loan: 'Позикові кошти',
            overlimit: 'Овердрафт',
            overdue: 'Прострочено',
            creditLimit: 'Виданий кредитний ліміт',
            finBlocking: 'Фінансове блокування',
            interests: 'Відсотки',
            penalty: 'Неустойка',
            minimalPayment: 'Мінімальний платіж',
            mandatoryPayment: 'Обов\'язковий платіж',
            unusedCreditLimit: 'Невикористаний кредитний ліміт',
            fee: 'Нараховано відсотків та інше',
            fullOwn: 'Власні кошти (загальна сума включно зі скарбничками)',
            debt: 'Повна заборгованість'
          },
          clientCardDetail: {
            label: 'Основні дані',
            fullName: 'Ім\'я',
            from: 'з',
            status: {
              label: 'Статус'
            },
            category: 'Категорія',
            available: 'Доступно',
            minimumBalance: 'Незнижуваний залишок',
            creditLimit: 'Кредитний ліміт',
            usedCreditLimit: 'Використано кредиту',
            balance: 'Баланс',
            balances: 'Баланси',
            type: 'Тип',
            hasChip: {
              label: 'Чипована',
              yes: 'Так',
              no: 'Ні'
            },
            barcode: 'Штрих-код',
            view: 'Вид',
            registrationPlace: 'Місце оформлення',
            department: 'Відділення',
            address: 'Адреса',
            phone: 'Телефон',
            viewIsVirtual: {
              virtual: 'Віртуальна',
              plastic: 'Пластикова'
            }
          },
          clientCardTableList: {
            columns: {
              number: '№ картки',
              expired: 'Термін',
              embossedName: 'Ім\'я',
              smsInfoPhone: 'SMS-info',
              pushInfoPhone: 'Push-info',
              securePhone: '3D-Secure',
              inStopList: 'Обмеження',
              status: 'Статус'
            },
            limit: {
              yes: 'Так',
              no: 'Немає'
            },
            services: {
              yes: 'Так',
              no: 'Ні'
            }
          },
          slider: {
            cardSliderInfo: {
              agreement: 'Договір',
              limit: {
                yes: 'Так',
                no: 'Немає'
              }
            }
          },
          clientCardServices: {
            status: 'Статус',
            phone: 'Телефон',
            tariff: 'Тариф',
            lastChange: 'Дата та час останньої зміни',
            statusChangedBy: 'Змінив статус',
            notFinPhone: 'Інший номер'
          },
          services: {
            cardSmsInfo: {
              toggleTurnOn: 'Підключити SMS-info',
              toggleTurnOff: 'Відключити SMS-info'
            }
          },
          clientCardList: {
            title: 'Картки',
            titleFiltered: 'Картки по договору',
            agreement: 'Договір',
            cardInfo: 'Інформація по картці',
            balance: 'Баланс',
            restrictions: 'Обмеження',
            status: 'Статус',
            notActive: 'Не підключено'
          },
          clientCardSearch: {
            search: 'Пошук',
            searchFocus: 'Пошук у картках ...'
          },
          tokens: {
            clientCardTokenList: {
              columns: {
                id: '#',
                deviceName: 'Назва пристрою',
                wallet: 'Назва гаманця',
                status: 'Статус'
              },
              statusDetail: {
                label: 'Деталі статусу',
                title: 'Статус токену',
                lastStatusDate: 'Дата та час встановлення останнього статусу',
                changedBy: 'Змінив статус',
                reason: 'Причина'
              },
              changeStatus: 'Змінити статус на',
              confirmActivate: 'Ви дійсно хочете активувати токен?',
              cancelActivate: 'Відміна',
              activateToken: 'Активувати',
              actionResult: {
                positive: 'Статус токена змінено!',
                negative: 'Помилка зміни статуса токена.'
              }
            },
            clientCardTokenChangeStatus: {
              label: 'Причина зміни статус',
              commentHint: 'Написати причину',
              attention: 'Увага, після видалення токен відновити буде неможливо!',
              deleteBtnLabel: 'Видалити токен',
              actionResult: {
                positive: 'Статус токена змінено!',
                negative: 'Помилка зміни статуса токена.'
              },
              blockMobileApp: {
                label: 'Заблокувати доступ в моб. додаток?',
                yes: 'Так',
                no: 'Ні'
              }
            }
          },
          audit: {
            cardStatusHistory: {
              title: 'Історія. Статус картки',
              status: 'Поточний статус',
              futureStatus: 'Майбутній статус',
              emissionDate: 'Дата та час емісії',
              commentsTitle: 'Коментарі',
              date: 'Дата та час зміни статусу',
              comment: 'Причина зміни статусу'
            },
            secure3DStatusHistory: {
              title: 'Історія. Статус 3D-Secure',
              status: 'Поточний статус',
              date: 'Дата та час останньої зміни',
              comment: 'Статус змінив'
            },
            smsInfoStatusHistory: {
              title: 'Історія. Статус Sms-Info',
              status: 'Поточний статус',
              date: 'Дата та час останньої зміни',
              comment: 'Статус змінив'
            }
          },
          limits: {
            cardLimits: {
              limits: 'Ліміти',
              blockCard: 'Заблокувати картку',
              cashWithdrawalLimit: 'Ліміт на зняття готівки',
              operations: 'Операції в ОБ / МП',
              internalOperations: 'Операції між рахунками одного Клієнта',
              operationsPerDay: 'Операції на добу',
              operationsPerWeek: 'Операції на тиждень',
              operationsPerMonth: 'Операції на місяць',
              operationsPerYear: 'Операції на рік',
              transferOperations: 'Операції з переказу коштів в межах та за межі банку',
              maxAmountPerOneOperation: 'Максимальна сума однієї операції',
              limitPerDay: 'Ліміт на добу',
              limitPerWeek: 'Ліміт на тиждень',
              limitPerMonth: 'Ліміт на місяць',
              limitPerYear: 'Ліміт на рік',
              cardLimits: 'Карткові',
              systemLimit: 'Системні',
              riskyLimit: 'Доступно в ризикових країнах в еквіваленті на зняття готівки та переказу картки',
              products: 'Продуктові ліміти',
              virtual: 'Віртуальна картка',
              unableToSetStatus: 'Неможливо змінити статус ліміту!'
            },
            cardChangeSystemLimit: {
              changeSystemLimit: 'Зміна ліміту',
              changeSystemLimitInfo: 'Сума для зняття готівки в ризикових країнах',
              change: 'Змінити',
              cancel: 'Скасувати',
              infoWillBePass: 'Інформація буде передана у відділ моніторингу операцій з ПК',
              amount: 'Сума, UAH',
              phone: 'Номер телефону',
              changeP2PLimit: 'Змінити ліміт переказу p2p в ризикових країнах'
            },
            cardCannotUnlocked: {
              titleUnableUnlock: 'Картку неможливо розблокувати',
              showDetails: 'Переглянути деталі',
              timeOfBlocking: 'Дата та час блокування',
              reasonOfBlocking: 'Причина',
              titleGetNew: 'Способи отримати нову картку',
              way1: 'Прийти до відділення з паспортом та ІПН, де менеджер запропонує нову картку;',
              way2: 'Замовити картку онлайн',
              way3_1: 'Завантажити наш новий додаток "Банк Власний Рахунок" за посиланням',
              way3_2: 'та отримати картку з багатьма перевагами.'
            },
            cardMightUnlocked: {
              unlockCard: 'Розблокувати картку'
            }
          },
          debtToBank: {
            cardInGracePeriod: 'Картка знаходиться в пільговому періоді.',
            cardInGracePeriodUntil: 'Картка знаходиться в пільговому періоді до',
            interestNotAccrued: 'Відсотки за користування кредитом не нараховуються.',
            preferentialPayment: 'Сума для поповнення, щоб залишатися в пільговому періоді',
            forUsingGracePeriodPayment: 'Сума для поповнення, щоб почати користуватися пільговим періодом',
            mandatoryPayment: 'Мінімальний платіж',
            penalty: 'Штрафні санкції',
            accruedInterest: 'Сума нарахованих відсотків ({percent} на місяць)',
            debtCalcPercentPerMonth: 'Заборгованість нараховується {percent} на місяць',
            until: 'До',
            afterEndCreditVacation: 'Після закінчення «кредитних» канікул (на поточний момент, це з 01.06.21)',
            willBeCharged: 'Буде списано',
            perMonth: 'за місяць:',
            credited: 'Зараховано',
            notCredited: 'Не зараховано',
            afterGracePeriodWillBeСredited: 'Після виходу з пільгового періоду буде нараховано',
            forMaxAmountDebtEachDay: '- на максимальну суму заборгованості кожного дня',
            fromFirstDayCurrentDebtPeriod: '- починаючи с першого дня виникнення заборгованості у поточному пільговому періоді',
            creditVacationPeriodInfo: 'На період дії «кредитних» канікул, повідсотки та штрафні санкції не нараховуються згідно з чинним законодавством.',
            creditLimitNotUsed: 'Кредитний ліміт що не запропоновано або не використовується'
          }
        },
        travels: {
          finish: 'Завершити',
          clientTravelList: {
            title: 'Поїздки',
            createNewTravel: 'Створити поїздку',
            noData: 'Поїздок немає',
            confirmDeleteTravel: 'Ви дійсно хочете видалити поїздку?',
            cancel: 'Відміна',
            delete: 'Видалити',
            columns: {
              travelId: '№',
              status: 'Статус',
              period: 'Період',
              country: 'Країна'
            }
          },
          clientTravelInfo: {
            title: 'Поїздка №',
            status: 'Статус',
            countries: 'Країни',
            period: 'Період перебування',
            contactPhone: 'Контактний телефон',
            cardMasks: 'Маска картки',
            setRiskyLimit: 'Встановити ліміти в ризиковій країні',
            p2pLimit: 'Ліміт на перекази з картки',
            notChangeLimits: 'Ліміти не змінювати'
          },
          clientTravelChooseCards: {
            instruction: 'Оберіть картку або картки, якими клієнт планує користуватися за кордоном',
            columns: {
              cardNumber: 'Номер картки',
              cardName: 'Назва картки',
              agreementId: '№ Договору'
            },
            errors: {
              requiredСard: 'Оберіть хоча б одну картку'
            }
          },
          clientTravelNew: {
            createTravel: 'Створити поїздку',
            cards: 'Картки',
            clientPlanningTrip: 'Клієнт планує поїздку',
            clientAlreadyAbroad: 'Клієнт вже знаходитися за кордоном',
            whichСountryTripPlanned: 'Підкажіть, будь ласка, в яку країну Ви плануєте поїздку?',
            whichСountryThereClient: 'Підкажіть, будь ласка, в якій країні Ви знаходитесь?',
            stayPeriod: 'Період перебування',
            foreignNumber: 'Номер телефону',
            stayCountries: 'Країни перебування',
            allFieldsRequired: 'Всі поля обов\'язкові до заповнення',
            search: 'Пошук',
            riskyCountry: 'Ризикова країна',
            errors: {
              requiredIsClientAbroad: 'Необхідно вказати клієнт закордоном або планує поїздку',
              requiredСountry: 'Необхідно вказати хоча б одну країну перебування',
              requiredPhone: 'Необхідно вказати контактний номер',
              requiredPeriod: 'Необхідно вказати період перебування'
            }
          },
          clientTravelChangeLimit: {
            isAriskyCountry: '- це ризикова країна.',
            isAriskyCountries: '- це ризиковані країни.',
            limitsInIt: 'У ній',
            limitsInThem: 'У них',
            limitsInRiskyCountry: 'ліміти на зняття готівки та перекази з картки складають 500 гривень на добу в еквіваленті.',
            no: 'Ні',
            yes: 'Так',
            cangeRiskyLimit: 'Бажаєте змінити ліміти на операції в ризиковій країні?',
            setRiskyLimit: 'Вкажіть бажаний ліміт на зняття готівки та перекази з картки',
            redirectCall: 'Щоб оперативно виконати Ваш запит, я зараз переведу дзвінок у відділ моніторингу платіжних карток. Залишайтесь, будь ласка, на лінії',
            hasMaintananceAnswered: 'Спеціаліст фін.моніторингу відповів на дзвінок?',
            cashWithdrawalLimit: 'Зняття готівки',
            limitCardTransfers: 'Перекази з картки',
            errors: {
              requiredIsHelp: 'Необхідно вказати "Спеціаліст фін.моніторингу відповів на дзвінок?"',
              cashWithdrawalLimitLess500UAH: 'Ліміт зняття готівки не може бути менше 500 грн',
              limitCardTransfersLess500UAH: 'Ліміт на перекази з картки не може бути менше 500 грн'
            }
          },
          clientTravelSuccessfullyCreated: {
            successfullyCreated: 'Поїздка успішно створена',
            countries: 'Країни',
            period: 'Період перебування',
            contactPhone: 'Контактний телефон',
            cardMask: 'Маска картки',
            setLimitInRiskyCountry: 'Встановити ліміт в ризиковій країні',
            cashWithdrawallLimit: 'Ліміт на зняття готівки',
            p2pLimit: 'Ліміт на перекази з картки',
            limitsWillBeChange: 'Ліміти на операції в ризиковій країні будуть змінені відповідно до вашого запиту і будуть діяти протягом зазначеного періоду',
            notChangeLimits: 'Ліміти не змінювати'
          }
        },
        clientActivity: {
          filterLabel: 'Фільтри',
          showMore: 'Показати ще',
          noMoreData: 'Більше немає даних...',
          filter: {
            title: 'Фільтр по',
            dateTitle: 'Дата',
            calendar: {
              close: 'Застосувати'
            },
            cardNumLabel: 'Останні 4 цифри',
            amountFromLabel: 'Від',
            amountToLabel: 'До',
            addNewFilter: 'Додати фільтр',
            applyFilter: 'Застосувати',
            clearFilter: 'Скасувати'
          },
          filterTypeEnum: {
            dateRange: 'Період',
            card: 'Картка',
            operation: 'Операції'
          },
          available: 'Залишок',
          dateTime: 'Дата та час',
          merchant: 'Торговець',
          mcc: 'MCC',
          terminalId: 'ID терміналу',
          accountAmount: 'Сума у валюті рахунку',
          commission: 'Комісія',
          status: 'Статус',
          responseCode: 'Код відповіді',
          authCode: 'Код авторизації',
          paymentMethod: 'Спосіб оплати',
          info: 'Інфо',
          acqInstitutionCode: 'Банк-еквайєр',
          terminalType: 'Тип терміналу',
          cardDataInputMode: 'Спосіб прийому карти',
          pinVerification: 'Перевірка ПІН',
          cvv2Verification: 'Перевірка CVV2',
          cavvValidation: 'Перевірка CAVV (3DS)',
          disputeOperation: 'Оскаржити операцію',
          childCount: '0 | {n} операція | {n} операції | {n} операцій',
          reverse: 'Повернення',
          expenseTransaction: 'Видаткова операція'
        },
        clientActivityItemFees: {
          feeTypeName: 'Тип комісії',
          feeAmount: 'Сума'
        }
      },
      tariffs: {
        tariffDetail: {
          edition: 'Редакція тарифів діюча',
          sendTariff: 'Надіслати тариф',
          finPhone: 'Фінансовий номер',
          notSet: 'Не вказано',
          sendBy: 'Надіслати'
        }
      },
      statement: {
        statementDetail: {
          edition: 'Формування виписки',
          sendStatement: 'Надіслати виписку',
          notSet: 'Не вказано',
          setStatement: 'Виписка',
          period: 'Період'
        }
      },
      wizard: {
        wizard: {
          limitLabel: 'Блокування картки',
          chooseBlockReason: 'Виберіть причину блокування картки',
          reasonBlocking: 'Вкажіть причину блокування',
          enterData: 'Введіть дані особи, яка знайшла картку',
          enterDataPersonWhoCall: 'Введіть дані особи, яка звернулася',
          enterCardNumberForCheck: 'Введіть номер картки для перевірки',
          chooseNotClientOperation: 'Виберіть операції, які зробив не клієнт',
          chooseCheaterOperation: 'Виберіть ймовірну шахрайську операцію',
          selectedValuesWillCancelRegistration: 'Вибрані значення видалять код доступу в додаток',
          findTokensWithNotActiveStatus: 'Знайдіть токени зі статусом «Неактивний» та видаліть активовані шахраями',
          selectDataBecameKnown: 'Виберіть дані, які стали відомі шахраям',
          outgoingCallTaskCreated: 'Завдання на вихідний дзвінок створено!',
          informationWasTransferred: 'Інформація передана у відділ моніторингу операцій з ПК',
          informationWasPreperred: 'Інформація для блокування картки зібрана і підготовлена',
          isClientCall: 'Дзвонить клієнт?',
          isClientLostPhone: 'Клієнт втратив телефон?',
          isClientToldSMCodeFromSMS: 'Клієнт повідомляв комусь код з SMS?',
          personWhoCallsDescription: 'Вкажіть причину, чому клієнт не може звернутися самостійно',
          cheaterInformatiomDescription: 'Напишіть яку інформацію повідомив вам шахрай',
          refusedToProvideInfo: 'Особа відмовилась надати інформацію',
          allClientOperations: 'Всі операції клієнта',
          noOperation: 'Операція відсутня',
          fullName: 'ПІБ',
          phone: 'Номер телефону',
          enterPhoneClient: 'Введіть телефон клієнта',
          useFinPhone: 'Використовувати фін. номер',
          selectOperationType: 'Визначте тип операції',
          selectSecureCodeOperationType: 'Визначте тип операції, для якої знадобився код',
          operationType: 'Тип операції',
          operationFromUserDevice: 'Операція була здійснена з телефону клієнта?',
          cardNumber: 'Номер картки',
          fraudDescription: 'Опишіть детально причину',
          changePinAndCallLater: 'Рекомендуємо змінити код блокування екрану, а якщо він не був встановлений - то встановити. Доступ в мобільний застосунок заблокований. Передзвоніть нам, коли ситуація проясниться, щоб розблокувати доступ. Зареєструйте звернення на сайті кіберполіції. З номером заяви з сайту кіберполіції в Банку можна буде оформити заяву про спірну операцію.',
          callMobileOperatorAndBlockSim: 'Якщо вам незнайомий цей пристрій, є ймовірність, що шахраї отримали доступ до вашого фінансового номеру. Зв\'яжіться зі службою підтримки мобільного оператора і заблокуйте  SIM, якщо номер вкрадений. Наступний крок - це реєстрація звернення на сайті кіберполіції. З номером заяви з сайту кіберполіції в Банку можна буде оформити заяву про спірну операцію',
          mobileAppAccessBlocked: 'Доступ в мобільний застосунок заблокований. Передзвоніть нам, коли ситуація проясниться, щоб розблокувати доступ',
          thingsFound: 'Знайдено речі',
          writeThingsFound: 'Напишіть, які речі були знайдені',
          allFieldsRequired: 'Всі поля обов\'язкові до заповнення',
          cardNotBelongBank: 'Картка не належить ПАТ "БАНК ВОСТОК"',
          cardBlockSuccess: 'Картка успішно заблокована',
          cancel: 'Скасувати',
          back: 'Назад',
          continue: 'Продовжити',
          done: 'Закрити',
          blockCard: 'Заблокувати картку',
          details: 'Детальна інформація',
          clientConfirmOperations: 'Клієнт підтвердив операції',
          cardWillbeBlocked: 'Картка буде заблокована натиснувши на кнопку "Заблокувати картку"',
          transactionsNotPossible: 'Операції за карткою неможливі',
          transactionsNotPossibleReissue: 'Операції за карткою неможливі. Вона підлягає перевипуску',
          transactionsNotPossibleСlarifyDetails: 'Операції за карткою неможливі. Протягом однієї доби клієнту зателефонують представники моніторингу банку для уточнення деталей.',
          transactionsNotPossibleCanBeUnlocked: 'Операції за карткою неможливі. Клієнт може розблокувати її самостійно в мобільному додатку, або звернувшись до контакт-центру',
          meetingAgreement: 'Щоб домовитись про зустріч і повернути знайдені речі банк повідомить клієнту телефон особи, яка звернулася',
          destroyCard: 'ЇЇ необхідно розрізати та викинути. Банк зв’яжеться з клієнтом та повідомить, як перевипустити карту',
          cardGotToTheThirdPerson: 'Оскільки картка потрапила до рук 3-х осіб, банк рекомендує перевипуск',
          infoWillBeTransferred: 'Інформація буде передана в службу безпеки банку. Особі, яка звернулася зателефонують протягом одного робочого дня',
          clientLostPhoneInfo: 'З метою безпеки реєстрація та вхід в мобільний додаток заблоковані. Після відновлення номера доступ можна буде розблокувати, звернувшись до контакт-центру',
          mobileAppPinResetInfo: 'В цілях безпеки відмінена реєстрація в мобільному додатку. Для входу необхідно повторно зареєструватися',
          enterDataAboutCheater: 'Введіть дані про шахрая',
          cheaterIntroducedHimself: 'Ким представився',
          callDate: 'Дата дзвінка',
          writeDataFromCheater: 'Напишіть яку інформацію повідомив вам шахрай',
          cancelRegistrationInMobileApp: 'Також задля безпеки відмінена реєстрація в мобільному додатку. Для входу необхідно повторно зареєструватись. Зверніть увагу, що нікому не можна повідомляти коди з СМС, співробітники банку ніколи не уточнюють подібну інформацію',
          enterDataAboutSMSInUPC: 'Вкажіть дату та час відправки повідомлення в SMS трекінгу (UPC)',
          enterDataAboutSMSInGate: 'Вкажіть дату та час відправки повідомлення в SMS Gate',
          SMSMissing: 'Повідомлення відсутнє',
          specifySMSSenderName: 'Уточніть у клієнта назву відправника в СМС',
          senderNameInSMS: 'Назва відправника в СМС',
          provideInformationFromClient: 'Вкажіть зі слів клієнта інформацію',
          merchantName: 'Назва торговця',
          bankRecommendsBlockingCard: 'Шахраї не змогли завершити операцію без секретного коду з СМС. Але сам факт, що спроба була, означає, що дані картки скомпрометовані. Банк рекомендує заблокувати картку та перевипустити нову',
          customerAgreeBlockCard: 'Клієнт згоден на блокування картки?',
          askSendScreenShot: 'Попросіть клієнта відправити скрін екрану з повідомленням на',
          notBVROperation: 'Це не операція по картці Банк Власний Рахунок. Усі повідомлення від Банку надходять з альфа-імені BANKVOSTOK',
          securityCode: {
            mobilePasswordAppInfo: 'Шахраї намагались (зареєструватись, увійти, провести операцію, інше) в МЗ, але не змогли завершити операцію без секретного коду з СМС. Сам факт, що спроба була, означає, що дані картки та персональні дані скомпрометовані. Банк рекомендує заблокувати картку та перевипустити нову. За бажанням клієнта картка може не блокуватися. Але в разі проведення операцій - Банк не зможе розглянути заяву про шахрайські операції і ініціювати розслідування для повернення коштів.',
            cardVerificationTokenInfo: 'Знайдіть токени зі статусом «Неактивний» та видаліть активовані шахраями',
            cardVerificationInfo: 'Шахраї намагались додати картку в систему електронних платежів, але не змогли завершити операцію без секретного коду з СМС. Сам факт, що спроба була, означає, що дані картки скомпрометовані Банк рекомендує заблокувати картку та перевипустити нову. За Вашим бажанням картка може не блокуватися. Але в разі проведення операцій - Банк не зможе розглянути заяву про шахрайські операції і ініціювати розслідування для повернення коштів',
            otherCodeInfo: 'Шахраї не змогли завершити операцію без секретного коду з СМС. Але сам факт, що спроба була, означає, що дані картки скомпрометовані. Зверніть увагу, що деякі сайти не підтримують послугу 3D-Secure Тобто, операції будуть проводитися без підтверджуючих паролів. Відповідальність за несанкціоновані операції буде нести сайт. Ви зможете написати заяву про спірну операцію та повернути кошти Але термін розгляду таких заяв довготривалий та може затягнутися до 180 днів. Щоб виключити можливі несанкціоновані операції по карті, Банк рекомендує заблокувати скомпрометовану картку та переоформити'
          },
          employeesNeverAsk: {
            labelTop: 'Пам’ятайте, що співробітники Банку ніколи не телефонують клієнтам та не запитують',
            data1: 'Термін дії картки',
            data2: 'Три цифри на звороті картки',
            data3: 'Будь-які паролі',
            data4: 'Ваші персональні дані (паспортні дані, кодове слово, прізвище, ім’я, по-батькові, дату народження)',
            labelBottom: 'Нікому не повідомляйте ці дані'
          },
          clientAsksAboutUnlocking: {
            label: 'Якщо клієнт сам запитає про можливість розблокувати',
            line1: 'Тимчасово для переказу готівки на іншу картку – повідомити, що він може зробити це самостійно в мобільному додатку або звернувшись до Контакт-центру. Після переказу картку необхідно повторно заблокувати',
            line2: 'На постійній основі – повідомити, що він може зробити це самостійно в мобільному додатку або звернувшись до Контакт-центру, але банк не рекомендує цього робити, т.я. дані з картки були скомпрометовані'
          },
          columns: {
            dateTime: 'Дата та час',
            merchantName: 'Назва торговця',
            operationDetails: 'Деталі операції',
            amount: 'Валюта і сума',
            status: 'Статус'
          },
          reasons: {
            other: 'Інше',
            foundbByAnotherPerson: 'Знайдена іншою особою',
            lost: 'Загублена',
            stolen: 'Вкрадена',
            forgottenInATM: 'Забута в банкоматі',
            unknownTransactions: 'Операції без відома клієнта',
            reportedData: 'Повідомив дані шахраям',
            clientIsCheater: 'Клієнт банку - шахрай',
            withdrawnFromATM: 'Вилучена в банкоматі',
            blockingOperations: 'Невпевнений, що картка втрачена',
            requestedByClient: 'Блокування за рішенням клієнта (картка в наявності)',
            receivedSMSCode: 'Отримав SMS з кодом'
          },
          selectedColumns: 'Нічого не обрано | Обрано {n} операцію | Обрано {n} операції | Обрано {n} операцій',
          confirmMessage: 'Обрані операції будуть очищені, якщо вибрати інший період. Продовжити?',
          confirm: {
            yes: 'Так',
            no: 'Ні'
          },
          calendar: {
            close: 'Застосувати'
          },
          errors: {
            checkPersonalData: {
              requiredAny: 'Необхідно вибрати хоча б один пункт'
            },
            choiceClientTransactions: {
              requiredAny: 'Необхідно вибрати хоча б одну операцію'
            },
            сardFinderDetails: {
              requiredFullName: 'Необхідно вказати ПІБ',
              requiredPhone: 'Необхідно вказати номер телефону',
              requiredThingsDesc: 'Необхідно написати які речі були знайдені'
            },
            cardNumber: {
              requiredCardNumber: 'Необхідно ввести номер картки'
            },
            fraudFinderDetails: {
              requiredFullName: 'Необхідно вказати ПІБ',
              requiredPhone: 'Необхідно вказати номер телефону',
              requiredCardNumber: 'Необхідно ввести номер картки',
              requiredDescription: 'Необхідно детально описати причину'
            },
            personalWhoCallsDetails: {
              requiredFullName: 'Необхідно вказати ПІБ',
              requiredPhone: 'Необхідно вказати номер телефону',
              requiredDescription: 'Необхідно детально описати причину'
            },
            cardTransactionsType: {
              requiredPhone: 'Необхідно вказати номер телефону',
              requiredTransactionsType: 'Визначте тип операції',
              requiredFromClientMobileApp: 'Визначте, операція була здійснена з телефону клієнта?'
            },
            cheaterDetails: {
              requiredFullName: 'Необхідно вказати ПІБ',
              requiredPhone: 'Необхідно вказати номер телефону',
              requiredDateCall: 'Необхідно вказати дату дзвінка',
              requiredDataFromCheater: 'Напишіть яку інформацію повідомив вам шахрай'
            },
            cardBlockSMSSenderName: {
              requiredSenderName: 'Необхідно вказати назву відправника',
              requiredMerchantName: 'Необхідно вказати назву торговця',
              requiredDate: 'Необхідно вказати дату та час',
              requiredClientAgree: 'Необхідно вказати: Клієнт згоден на блокування картки?'
            },
            cardBlockSMSDetailUPC: {
              requiredDate: 'Необхідно вказати дату та час',
              requiredClientAgree: 'Необхідно вказати: Клієнт згоден на блокування картки?'
            },
            cardBlockSMSDetailGate: {
              requiredDate: 'Необхідно вказати дату та час'
            },
            cardSecurityCodeOperationType: {
              requiredOperationType: 'Визначте тип операції'
            }
          }

        },
        unlocking: {
          unlockingLabel: 'Розблокування картки',
          clientNearATMorСashbox: 'Клієнт зараз знаходиться біля банкомату або в касі Банку?',
          stayNearATMorCashbox: 'Залишайтеся, будь ласка, на лінії поки знімаєте необхідну суму. Після зняття - картка буде заблокована.',
          contactLaterNearATMorCashbox: 'Зверніться, будь ласка, повторно, коли будете біля банкомату або в касі банку.',
          enterContactPhoneNumber: 'Введіть контактний номер телефону',
          financialPhoneСonfirmed: 'Фінансовий номер підтверджено',
          financialPhoneNotСonfirmed: 'Фінансовий номер не підтверджений',
          financialPhone: 'Фінансовий номер',
          contactPhone: 'Контактний номер',
          cardMask: 'Маска картки',
          dialogId: 'ID діалогу',
          notEnoughInformationToUnlock: 'Недостатньо інформації для розблокування картки',
          operationsNotConfirmed: 'Операції не підтверджені.',
          specifyReasonNonConfirmation: 'Вкажіть причину непідтвердження операцій *',
          clientConfirmsThatNotPerformOperation: 'Клієнт підтверджує, що не скоював операції',
          methodsCardReissueProvided: 'Надано способи перевипуску картки',
          recommendedContactPolice: 'Рекомендовано оформити заяву в поліцію/кіберполіцію',
          informedPossibleCall: 'Проінформований про можливий дзвінок від фахівця відділу Моніторингу',
          cardBlocked: 'Картка заблокована',
          cardUnlocked: 'Картка розблокована',
          operationsConfirmed: 'Операції підтверджені',
          dataConfirmed: 'Дані підтверджені',
          checkInformationAboutWithdrawals: 'Уточніть у клієнта інформацію про зняття коштів. Якщо клієнт зняв кошти, заблокуйте картку',
          checkFromClientToConfirm: 'Уточніть у клієнта дані для підтвердження',
          clientConfirmData: 'Клієнт підтвердив дані?',
          clientDifficultToAnswer: 'Клієнту важко відповісти?',
          unlockCard: 'Розблокувати картку',
          lockCard: 'Заблокувати картку',
          finish: 'Завершити',
          errors: {
            cardFindNumberCheck: {
              requiredPhone: 'Необхідно вказати коректний номер телефону'
            },
            cardConfirmFromClient: {
              requiredDataConfirmed: 'Необхідно заповнити "Клієнт підтвердив дані?"',
              requiredDifficultToAnswer: 'Необхідно заповнити "Клієнту важко відповісти?"'
            },
            clientNearATM: {
              requiredNearATM: 'Необхідно вказати відповідь'
            },
            cardDialogInsufficientData: {
              requiredReason: 'Необхідно вказати причину непідтвердження операцій'
            }
          }
        }
      }
    },
    shared: {
      yes: 'Так',
      no: 'Ні',
      dateAndTime: 'Дата та час',
      pager: {
        label: 'з'
      },
      errorPopup: {
        title: 'Помилка',
        text: 'Виникла помилка.',
        textSession: 'ID помилки: ',
        countLabel: 'Загальна кількість помилок:',
        buttons: {
          showDetail: 'Докладніше',
          send: 'Надіслати розробникам',
          cancel: 'Закрити'
        },
        retailApiError: 'Сталася помилка на стороні Retail System спробуйте повторити ваш запит через хвилину'
      },
      noData: {
        label: 'Даних не знайдено'
      },
      noDataSmall: {
        label: 'Немає даних'
      },
      errorData: {
        label: 'Виникла помилка'
      },
      blockSpinner: {
        label: 'Завантаження даних'
      },
      validation: {
        requiredField: 'Поле обов\'язкове для заповнення'
      }
    },
    temporary: {
      notImplement: {
        label: 'Розділ в розробці'
      }
    }
  },
  enums: {
    undefined: 'Невизначений',
    turnOn: 'Увімкнено',
    turnOff: 'Вимкнено',
    accountStatusEnum: {
      inactive: 'Закритий',
      active: 'Активний'
    },
    agreementStatusEnum: {
      active: 'Активний',
      suspended: 'Призупинений',
      frozen: 'Заморожений',
      closed: 'Закритий',
      reserve: 'Списано за рахунок резерву'
    },
    agreementTypeEnum: {
      cardAccount: 'Договір карткового рахунку'
    },
    agreementCategoryTypeCodeEnum: {
      inactiveAgreement: 'Неактивний рахунок',
      blockLimitArrest: 'Частковий арешт на суму',
      blockFullArrest: 'Повний арешт',
      blockFmDebit: 'Фінмон блок на списання',
      blockFmCredit: 'Фінмон блок на поповнення',
      blockFmFull: 'Фінмон блок повний',
      blockFmIdentification: 'Повторна ідентифікація',
      blockDecisionBank: 'Блок за рішенням банку',
      blockGrodi: 'ДРОРМ',
      blockGna: 'ДПА',
      blockTerrorist: 'Терорист',
      blockPep: 'ПЕП',
      blockDeathClient: 'Смерть клієнта',
      blockOverdueLoan: 'Прострочення по кредиту',
      blockCustom: 'Призначена для користувача блокування',
      blockResidentStatusChange: 'Зміна резидентності клієнта',
      blockClientMissinLists: 'Клієнт знайдений в списках безвісти зниклих',
      blockInvalidPassport: 'Дані про недійсному паспорту'
    },
    moneyBoxStatusEnum: {
      active: 'Активна',
      inactive: 'Закрита'
    },
    segmentTypeEnum: {
      vip: 'Vip',
      priority: 'Priority',
      employee: 'Employee',
      individual: 'Individual',
      future: 'Future',
      lost: 'Lost'
    },
    cardCategoryEnum: {
      primary: 'Основна',
      supplementary: 'Додаткова'
    },
    cardStatusEnum: {
      active: 'Активна',
      blocked: 'Заблокована',
      canceled: 'Анульована',
      lost: 'Загублена',
      stolen: 'Вкрадена',
      suspend: 'Призупинена'
    },
    cardSmsInfoStatusEnum: {
      active: 'Підключена',
      inactive: 'Відключена'
    },
    card3DSecureStatusEnum: {
      active: 'Підключена',
      inactive: 'Відключена'
    },
    cardBlockTransactionTypeEnum: {
      other: 'Інша операція',
      mobileApp: 'Операція в моб. застосунку'
    },
    cardBlockSecurityCodeOperationTypeEnum: {
      other: 'Інший код',
      mobileAppPassword: 'Пароль мобільного додатку',
      cardVerification: 'Код підтвердження оцифровки картки в системі електронних платежів'
    },
    cardBlockSenderNameTypeEnum: {
      other: 'Інша назва або номер'
    },
    cardSecure3dStatusHistoryEnum: {
      inactive: 'Відключена',
      active: 'Підключена'
    },
    smsInfoHistoryStatusEnum: {
      inactive: 'Неактивна',
      active: 'Підключена'
    },
    cardToken: {
      statusEnum: {
        active: 'Активний',
        deleted: 'Видалений',
        inactive: 'Неактивний',
        suspended: 'Призупинений'
      },
      initiatorEnum: {
        issuer: 'Емітент',
        tokenRequestorWalletProvider: 'Запитувач токенів (постачальник гаманців)',
        cardholder: 'Власник картки',
        mobPINValidService: 'Mobile PIN Validatioin Service',
        mobPINChangeValidService: 'Mobile PIN Change Validatioin Service'
      },
      reasonCodeEnum: {
        auth: 'Власник карти успішно пройшов аутентифікацію до активації',
        lost: 'Власник карти повідомив про втрату пристрої / Власник карти підтвердив втрату пристрої',
        stolen: 'Власник карти повідомив про крадіжку пристрої / Власник карти підтвердив крадіжку пристрою',
        closed: 'Рахунок або карта закриті',
        found: 'Емітент або власник повідомив про відновлення контролю над пристроєм',
        transaction: 'Емітент або власник карти підтвердив відсутність шахрайських транзакцій з токеном / Емітент або власник карти підтвердив шахрайські транзакції з токеном',
        other: 'Інше'
      },
      deleteReasonCodeEnum: {
        lost: 'Втрата пристрою',
        stolen: 'Крадіжка пристрою',
        closed: 'Рахунок або карта закриті',
        transaction: 'Шахрайство',
        other: 'Інше'
      }
    },
    transaction: {
      categoryEnum: {
        advice: 'Порада',
        request: 'Запит',
        notification: 'Сповіщення',
        check: 'Перевірка'
      },
      classEnum: {
        financial: 'Фінансова транзакція',
        authorization: 'Авторизація'
      },
      directionEnum: {
        original: 'Оригінальна транзакція',
        adjustment: 'Коригування (часткова відміна)',
        reverse: 'Відміна'
      },
      directionClassEnum: {
        debit: 'Дебетування рахунку',
        credit: 'Кредитування рахунку'
      },
      entryTypeEnum: {
        transaction: 'Транзакція',
        fee: 'Комісія',
        credit_limit: 'Встановлення або зміна кредитного ліміту'
      },
      txnStatusEnum: {
        new: 'Нова',
        loaded: 'Завантажена',
        loadError: 'Помилка завантаження',
        prepared: 'Підготовлена',
        finished: 'Оброблена',
        rejected: 'Відхилена',
        closed: 'Закрита',
        preProcessing: 'Предобработана',
        shaded: 'Перевизначена',
        preAppliedAsFin: 'Оброблена як фінансова'
      }
    },
    personalDataTypeEnum: {
      personal: 'Персональні',
      identification: 'Ідентифікаційні',
      application: 'Мобільного додатку та онлайн-банку',
      card: 'Карткові',
      code: 'Код завершення операцІї',
      other: 'Інше'
    },
    travelStatusEnum: {
      active: 'Активна',
      finished: 'Завершена',
      deleted: 'Видалена',
      planned: 'Запланована',
      error: 'Помилкова'
    }
  },
  quasar: {
    dateUtil: {
      months: [
        'cічня',
        'лютого',
        'березня',
        'квітня',
        'травня',
        'червня',
        'липня',
        'серпня',
        'вересня',
        'жовтня',
        'листопада',
        'грудня'
      ]
    }
  },
  languageSwitcher: {
    label: 'Мова інтерфейсу'
  },
  search: {
    placeholder: 'Введіть дані пошуку',
    result: {
      title: 'Результат пошуку',
      noDataLabel: 'Нічого не знайдено'
    },
    labels: {
      inn: 'ІПН',
      initials: 'ПІБ',
      iban: 'IBAN',
      cardNumber: 'Номер картки',
      phone: 'Номер телефону',
      code: 'Штрих-код картки'
    },
    options: {
      hint: 'Тип пошуку',
      inn: 'ІПН',
      initials: 'ПІБ',
      iban: 'IBAN',
      cardNumber: 'Номер картки',
      phone: 'Номер телефону',
      code: 'Штрих-код картки'
    },
    validations: {
      emailValidationError: 'Некоректний Email'
    }
  },
  client: {
    info: {
      title: 'Дані клієнта',
      inn: 'ІПН',
      dateOfBirth: 'Дата народження',
      birthPlace: 'Місце народження',
      codeWord: 'Кодове слово'
    },
    documents: {
      title: 'Паспортні дані',
      series: 'Серія',
      number: 'Номер паспорту',
      issueDate: 'Дата видачі',
      expireDate: 'Дійсний до',
      issueBy: 'Ким виданий',
      photoDate: 'Дата фото 25/45 років',
      сlosingDate: 'Дата закриття'
    },
    contact: {
      title: 'Контактні дані',
      financialPhone: 'Фінансовий номер'
    },
    address: {
      live: 'Адреса місця проживання',
      registration: 'Адреса місця реєстрації',
      city: 'Місто',
      area: 'Область',
      district: 'Район',
      street: 'Вулиця',
      house: 'Будинок',
      flat: 'Квартира'
    }
  },
  verification: {
    title: 'Верифікація',
    btnClose: 'Закрити'
  },
  errorPage: {
    message: 'Вибачте, тут нічого немає...',
    btnBack: 'Назад'
  }
};
